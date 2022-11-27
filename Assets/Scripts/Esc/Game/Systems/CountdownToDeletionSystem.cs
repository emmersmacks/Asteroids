using Esc.Game.Components;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class CountdownToDeletionSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DestroyDelayComponent>.Exclude<DestroyComponent> _delayGroup;
        
        public void Run()
        {
            foreach (var index in _delayGroup)
            {
                var delayEntity = _delayGroup.GetEntity(index);
                var currentDelay = delayEntity.Get<DestroyDelayComponent>().Value;

                if (currentDelay <= 0)
                {
                    delayEntity.Del<DestroyDelayComponent>();
                    delayEntity.Get<DestroyComponent>();
                    continue;
                }
                
                var newTime = currentDelay - Time.deltaTime;
                delayEntity.Replace(new DestroyDelayComponent() { Value = newTime });
            }
        }
    }
}