using Game.Components;
using Game.Extensions;
using Game.Views.SpawnPoints;
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

                var random = new System.Random();
                var randomIndex = random.Next(0, points.Length);
                var randomPoint = points[randomIndex];

                var transform = randomPoint.transform;
                var lookAngle = GetLookAngle(transform, randomPoint.TargetMoveDirection);

                _world.CreateBigAsteroid(randomPoint.transform.position, lookAngle);
                entity.Replace(new DelayComponent() { Value = 5 });
            }
        }

        private Vector3 GetLookAngle(Transform transform, Vector2 direction)
        {
            float signedAngle =
                Vector2.SignedAngle(transform.up, (direction - (Vector2)transform.position));

            var angles = transform.eulerAngles;
            angles.z += signedAngle;
            return angles;
        }
    }
}