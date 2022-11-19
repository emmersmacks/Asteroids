using Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class RotateSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, TransformComponent, RotateSpeedComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var directionComponent = entity.Get<DirectionComponent>();
                var transformComponent = entity.Get<TransformComponent>();
                var rotateSpeedComponent = entity.Get<RotateSpeedComponent>();

                var transform = transformComponent.Value;
                var direction = directionComponent.Value;
                var speed = rotateSpeedComponent.Value;

                transform.Rotate(new Vector3(0, 0, direction.x * speed * Time.deltaTime));

            }
        }
    }
}