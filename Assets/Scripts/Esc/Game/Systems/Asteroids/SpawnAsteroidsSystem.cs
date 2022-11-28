using Data.Parameters.Asteroids;
using Esc.Game.Components;
using Esc.Game.Components.SpawnPoints;
using Esc.Game.Components.Tags;
using Esc.Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems.Asteroids
{
    public class SpawnAsteroidsSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly AsteroidsSpawnParameters _spawnParameters = null;
        private readonly BigAsteroidParameters _bigAsteroidParameters = null;
        
        private readonly EcsFilter<DirectedSpawnPointsComponent, AsteroidTagComponent>.Exclude<DelayComponent, DestroyComponent> _spawnerGroup;
        
        public void Run()
        {
            foreach (var index in _spawnerGroup)
            {
                var entity = _spawnerGroup.GetEntity(index);
                
                var spawnPoints = entity.Get<DirectedSpawnPointsComponent>().Value;

                var random = new System.Random();
                var randomIndex = random.Next(0, spawnPoints.Length);
                var point = spawnPoints[randomIndex];

                var moveDirection = point.Direction;
                var transform = point.Point.transform;
            
                var lookAngle = GetLookAngle(transform, moveDirection);
                _world.CreateBigAsteroid(transform.position, lookAngle, _bigAsteroidParameters);
                
                var delayComponent = new DelayComponent() { Value = _spawnParameters.SpawnVo.SpawnDelay };
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