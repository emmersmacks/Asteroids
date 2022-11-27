using Esc.Game.Components;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class ForceMoveSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, RigidbodyComponent, SpeedComponent, TransformComponent, ForceMoveComponent>
            .Exclude<DestroyComponent> _moveGroup;

        public void Run()
        {
            foreach (var index in _moveGroup)
            {
                var moveEntity = _moveGroup.GetEntity(index);
                
                var direction = moveEntity.Get<DirectionComponent>().Value;
                var rigidbody = moveEntity.Get<RigidbodyComponent>().Value;
                var speed = moveEntity.Get<SpeedComponent>().Value;
                var transform = moveEntity.Get<TransformComponent>().Value;

                var globalDirection = transform.TransformDirection(Vector2.up);
                
                if(direction != Vector2.zero && direction.y > 0)
                    rigidbody.AddForce(globalDirection * speed);
            }
        }
    }
}