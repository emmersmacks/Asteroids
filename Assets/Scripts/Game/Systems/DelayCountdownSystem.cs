using Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class DelayCountdownSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DelayComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                var delayComponent = entity.Get<DelayComponent>();
                var currentDelay = delayComponent.Value;

                if (currentDelay <= 0)
                {
                    entity.Del<DelayComponent>();
                    continue;
                }

                
                var newTime = currentDelay - Time.deltaTime;
                entity.Replace(new DelayComponent() { Value = newTime });
            }
        }
    }
}