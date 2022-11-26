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
                
                var direction = entity.Get<DirectionComponent>().Value;
                var transform = entity.Get<TransformComponent>().Value;
                var rotateSpeed = entity.Get<RotateSpeedComponent>().Value;

                var zRotateAngle = direction.x * rotateSpeed * Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, zRotateAngle));
            }
        }
    }
}