using Game.Components;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class ForceMoveSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, RigidbodyComponent, SpeedComponent, TransformComponent, ForceMoveComponent>
            .Exclude<DestroyComponent> _group;

        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var direction = entity.Get<DirectionComponent>().Value;
                var rigidbody = entity.Get<RigidbodyComponent>().Value;
                var speed = entity.Get<SpeedComponent>().Value;
                var transform = entity.Get<TransformComponent>().Value;

                var globalDirection = transform.TransformDirection(Vector2.up);
                
                if(direction != Vector2.zero && direction.y > 0)
                    rigidbody.AddForce(globalDirection * speed);
            }
        }
    }
}