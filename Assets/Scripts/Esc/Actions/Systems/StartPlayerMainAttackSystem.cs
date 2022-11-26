using Actions.Components;
using Data;
using Data.Bases;
using Data.Parameters;
using Data.Parameters.PlayerBullet;
using Game.Components;
using Game.Components.SpawnPoints;
using Game.Components.Tags;
using Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Actions.Systems
{
    public class StartPlayerMainAttackSystem : IEcsRunSystem
    {
        private readonly IPlayerBulletParameters _playerBulletParameters = null;
        
        private readonly CustomEcsWorld _world = null;

        private readonly EcsFilter<StartPlayerMainAttackComponent> _actionGroup = null;
        private readonly EcsFilter<PlayerTagComponent, SpawnPointsWithBoolComponent, MainWeaponComponent> _weaponsGroup = null;

        public void Run()
        {
            foreach (var index in _actionGroup)
            {
                foreach (var weaponIndex in _weaponsGroup)
                {
                    var weaponEntity = _weaponsGroup.GetEntity(weaponIndex);
                    var spawnPointsComponent = weaponEntity.Get<SpawnPointsWithBoolComponent>();
                    var spawnPoints = spawnPointsComponent.Value;
                    if (spawnPoints[spawnPoints.Length - 1].IsSpawned)
                    {
                        for (int pointIndex = 0; pointIndex < spawnPoints.Length; pointIndex++)
                        {
                            spawnPoints[pointIndex] = new BoolSpawnPointBase() { Point = spawnPoints[pointIndex].Point, IsSpawned = false};
                        }
                    }

                    for (int i = 0; i < spawnPoints.Length; i++)
                    {
                        if (!spawnPoints[i].IsSpawned)
                        {
                            var weaponRotation = weaponEntity.Get<TransformComponent>().Value.rotation;
                            _world.CreateBullet(spawnPoints[i].Point.position, weaponRotation, _playerBulletParameters.DamageLayerMask);
                            spawnPoints[i] = new BoolSpawnPointBase() { Point = spawnPoints[i].Point, IsSpawned = true};
                            return;
                        }
                    }
                }
            }
        }
    }
}