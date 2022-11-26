using Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class DestroyDelayCountdownSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DestroyDelayComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                var currentDelay = entity.Get<DestroyDelayComponent>().Value;

                if (currentDelay <= 0)
                {
                    entity.Del<DestroyDelayComponent>();
                    entity.Get<DestroyComponent>();
                    continue;
                }
                
                var newTime = currentDelay - Time.deltaTime;
                entity.Replace(new DestroyDelayComponent() { Value = newTime });
            }
        }
    }
}