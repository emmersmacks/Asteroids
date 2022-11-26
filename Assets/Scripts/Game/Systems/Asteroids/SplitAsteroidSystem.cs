using Data;
using Game.Components;
using Game.Components.Asteroids;
using Game.Components.Tags;
using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems.Asteroids
{
    public class SplitAsteroidSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AsteroidTagComponent, DestroyComponent, AsteroidSizeComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                var asteroidSizeComponent = entity.Get<AsteroidSizeComponent>();
                var sizeType = asteroidSizeComponent.Value;
                
                if(sizeType == EAsteroidSizeType.Small)
                    continue;

                var transformComponent = entity.Get<TransformComponent>();
                var transform = transformComponent.Value;
                var rotationEuler = transform.rotation.eulerAngles;

                var zPositiveRotation = rotationEuler.z + 45;
                var zNegativeRotation = rotationEuler.z - 45;
                var positiveRotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, zPositiveRotation);
                var negativeRotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, zNegativeRotation);

                var position = transform.position;
                _world.CreateSmallAsteroid(position, positiveRotation);
                _world.CreateSmallAsteroid(position, negativeRotation);
            }
        }
    }
}