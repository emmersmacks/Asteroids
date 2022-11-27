using Data;
using Data.Parameters.Asteroids;
using Esc.Game.Components;
using Esc.Game.Components.Asteroids;
using Esc.Game.Components.Tags;
using Esc.Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems.Asteroids
{
    public class SplitAsteroidSystem : IEcsRunSystem
    {
        private const int RotateDegrees = 45;
        
        private readonly CustomEcsWorld _world = null;

        private readonly SmallAsteroidsParameters _smallAsteroidsParameters = null;
        private readonly EcsFilter<AsteroidTagComponent, DestroyComponent, AsteroidSizeComponent> _destroyedAsteroidsGroup;
        
        public void Run()
        {
            foreach (var index in _destroyedAsteroidsGroup)
            {
                var entity = _destroyedAsteroidsGroup.GetEntity(index);
                var asteroidSizeComponent = entity.Get<AsteroidSizeComponent>();
                var sizeType = asteroidSizeComponent.Value;
                
                if(sizeType == EAsteroidSizeType.Small)
                    continue;

                var transformComponent = entity.Get<TransformComponent>();
                var transform = transformComponent.Value;
                var rotationEuler = transform.rotation.eulerAngles;

                var zPositiveRotation = rotationEuler.z + RotateDegrees;
                var zNegativeRotation = rotationEuler.z - RotateDegrees;
                var positiveRotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, zPositiveRotation);
                var negativeRotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, zNegativeRotation);

                var position = transform.position;
                _world.CreateSmallAsteroid(position, positiveRotation, _smallAsteroidsParameters);
                _world.CreateSmallAsteroid(position, negativeRotation, _smallAsteroidsParameters);
            }
        }
    }
}