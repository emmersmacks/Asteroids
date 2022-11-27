using Esc.Game.Components;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class TransformMoveSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, SpeedComponent, TransformComponent, TransformMoveComponent>
            .Exclude<DestroyComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var direction = entity.Get<DirectionComponent>().Value;
                var speed = entity.Get<SpeedComponent>().Value;
                var transform = entity.Get<TransformComponent>().Value;
                
                var globalDirection = transform.TransformDirection(Vector2.up);
                var movePosition = transform.position + globalDirection;
                var moveDistance = speed * Time.deltaTime;
                
                if (direction != Vector2.zero)
                    transform.position = Vector2.MoveTowards(transform.position, movePosition, moveDistance);
            }
        }
    }
}