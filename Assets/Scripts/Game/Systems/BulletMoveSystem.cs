using Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class BulletMoveSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, SpeedComponent, TransformComponent, BulletComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var directionComponent = entity.Get<DirectionComponent>();
                var rigidbodyComponent = entity.Get<RigidbodyComponent>();
                var speedComponent = entity.Get<SpeedComponent>();
                var transformComponent = entity.Get<TransformComponent>();
                
                var direction = directionComponent.Value;
                var rigidbody = rigidbodyComponent.Value;
                var speed = speedComponent.Value;
                var transform = transformComponent.Value;

                var globalDirection = transform.TransformDirection(Vector2.up);

                if (direction != Vector2.zero && direction.y > 0)
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + globalDirection, speed * Time.deltaTime);
            }
        }
    }
}