using Actions.Components;
using Data;
using Data.Parameters;
using Game.Components;
using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Actions.Systems
{
    public class StartPlayerMainAttackSystem : IEcsRunSystem
    {
        private readonly IPlayerBulletParameters _playerBulletParameters = null;
        
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<StartPlayerMainAttackComponent> _actionGroup = null;
        private readonly EcsFilter<PlayerTagComponent, SpawnPointsComponent, MainWeaponComponent> _weaponsGroup = null;

        public void Run()
        {
            foreach (var index in _actionGroup)
            {
                var entity = _actionGroup.GetEntity(index);

                foreach (var weaponIndex in _weaponsGroup)
                {
                    var weaponEntity = _weaponsGroup.GetEntity(weaponIndex);
                    var spawnPointsComponent = weaponEntity.Get<SpawnPointsComponent>();
                    var spawnPoints = spawnPointsComponent.Value;
                    if (spawnPoints[spawnPoints.Length - 1].IsSpawned)
                    {
                        for (int pointIndex = 0; pointIndex < spawnPoints.Length; pointIndex++)
                        {
                            spawnPoints[pointIndex] = new BulletSpawnPointBase() { Point = spawnPoints[pointIndex].Point, IsSpawned = false};
                        }
                    }

                    for (int i = 0; i < spawnPoints.Length; i++)
                    {
                        if (!spawnPoints[i].IsSpawned)
                        {
                            var weaponRotation = weaponEntity.Get<TransformComponent>().Value.rotation;
                            _world.CreateBullet(spawnPoints[i].Point.position, weaponRotation, _playerBulletParameters.DamageLayerMask);
                            spawnPoints[i] = new BulletSpawnPointBase() { Point = spawnPoints[i].Point, IsSpawned = true};
                            return;
                        }
                    }
                }
            }
        }
    }
}