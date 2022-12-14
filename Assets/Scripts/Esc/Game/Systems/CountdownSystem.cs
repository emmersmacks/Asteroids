using Esc.Game.Components;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class CountdownSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DelayComponent>.Exclude<DestroyComponent> _delayGroup;
        
        public void Run()
        {
            foreach (var index in _delayGroup)
            {
                var entity = _delayGroup.GetEntity(index);
                var currentDelay = entity.Get<DelayComponent>().Value;

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