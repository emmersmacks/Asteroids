using Game.Components;
using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class SpawnAsteroidsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AsteroidSpawnPointsComponent>.Exclude<DelayComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                var asteroidSpawnPointsComponent = entity.Get<AsteroidSpawnPointsComponent>();
                var points = asteroidSpawnPointsComponent.Value;

                var randomIndex = Random.Range(0, points.Length);
                var randomPoint = points[randomIndex];

                _world.CreateBigAsteroid(randomPoint.transform.position, randomPoint.TargetMoveDirection);
                entity.Replace(new DelayComponent() { Value = 5 });
            }
        }
    }
}