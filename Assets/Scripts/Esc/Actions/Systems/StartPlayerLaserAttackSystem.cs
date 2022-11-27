using Data.Parameters.PlayerBullets;
using Esc.Actions.Components;
using Esc.Game.Components;
using Esc.Game.Components.SpawnPoints;
using Esc.Game.Components.Tags;
using Esc.Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Actions.Systems
{
    public class StartPlayerLaserAttackSystem : IEcsRunSystem
    {
        private readonly LaserWeaponParameters _laserWeaponParameters = null;
        
        private readonly CustomEcsWorld _world = null;

        private readonly EcsFilter<StartPlayerLaserAttackComponent> _actionGroup = null;
        private readonly EcsFilter<PlayerTagComponent, SpawnPointsWithBoolComponent, AdditionalWeaponComponent> _weaponsGroup = null;

        public void Run()
        {
            foreach (var index in _actionGroup)
            {
                foreach (var weaponIndex in _weaponsGroup)
                {
                    var weaponEntity = _weaponsGroup.GetEntity(weaponIndex);
                    
                    var chargesNumber = weaponEntity.Get<ChargesComponent>().Value;
                    
                    if(chargesNumber <= 0)
                        continue;
                    
                    var spawnPointsComponent = weaponEntity.Get<SpawnPointsWithBoolComponent>();
                    var spawnPoints = spawnPointsComponent.Value;
                    
                    weaponEntity.ReplaceComponent(new ShootComponent());

                    foreach (var point in spawnPoints)
                    {
                        var bullet = _world.CreateLaserBullet(point.Point.position,
                            point.Point.transform.TransformPoint(Vector3.up * _laserWeaponParameters.HitDistance), _laserWeaponParameters.DamageLayerMask);
                        bullet.Replace(new DestroyDelayComponent() { Value = _laserWeaponParameters.DestroyDelay });
                        
                        var damageLayer = bullet.Get<DamageLayerComponent>().Value;

                        var hits = Physics2D.RaycastAll(point.Point.position, point.Point.transform.TransformVector(Vector3.up), _laserWeaponParameters.HitDistance, damageLayer);
                        foreach (var hit in hits)
                        {
                            var uid = hit.collider.gameObject.GetInstanceID();
                            var hitEntity = _world.GetEntityWithUid(uid);
                            hitEntity.Get<DestroyComponent>();
                            hitEntity.Get<KilledTagComponent>();
                        }
                    }

                    var newChargesNumber = chargesNumber - _laserWeaponParameters.ChargeСost;
                    _world.LaserChargeChange(newChargesNumber);
                    var chargesComponent = new ChargesComponent() { Value = newChargesNumber };
                    weaponEntity.Replace(chargesComponent);
                }
            }
        }
    }
}