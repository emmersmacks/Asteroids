using Actions.Components;
using Data;
using Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Actions.Systems
{
    public class StartMainAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<StartMainAttackComponent, PlayerComponent> _actionGroup = null;
        private readonly EcsFilter<PlayerComponent, SpawnPointsComponent, MainWeaponComponent> _weaponsGroup = null;

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
                            spawnPoints[pointIndex] = new SpawnPointBase() { Point = spawnPoints[pointIndex].Point, IsSpawned = false};
                        }
                    }
                    
                    for (int i = 0; i < spawnPoints.Length; i++)
                    {
                        if (!spawnPoints[i].IsSpawned)
                        {
                            var bulletPrefab = Resources.Load<GameObject>("Bullet");
                            GameObject.Instantiate(bulletPrefab, spawnPoints[i].Point.position, Quaternion.identity);
                            spawnPoints[i] = new SpawnPointBase() { Point = spawnPoints[i].Point, IsSpawned = true};
                            return;
                        }
                    }
                }
            }
        }
    }
}