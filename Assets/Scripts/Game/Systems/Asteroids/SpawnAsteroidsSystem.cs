using Game.Components;
using Game.Components.SpawnPoints;
using Game.Components.Tags;
using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems.Asteroids
{
    public class SpawnAsteroidsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DirectedSpawnPointsComponent, AsteroidTagComponent>.Exclude<DelayComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var spawnPoints = entity.Get<DirectedSpawnPointsComponent>().Value;

                var random = new System.Random();
                var randomIndex = random.Next(0, spawnPoints.Length);
                var point = spawnPoints[randomIndex];

                var moveDirection = point.Direction;
                var transform = point.Point.transform;
            
                var lookAngle = GetLookAngle(transform, moveDirection);
                _world.CreateBigAsteroid(transform.position, lookAngle);
                
                var delayComponent = new DelayComponent() { Value = 3 };
                entity.Replace(delayComponent);
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