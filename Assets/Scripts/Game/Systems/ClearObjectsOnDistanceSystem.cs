using Game.Components;
using Leopotam.Ecs;

namespace Game.Systems
{
    public class ClearObjectsOnDistanceSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TransformComponent>.Exclude<DestroyComponent> _group = null;

        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);

                var transform = entity.Get<TransformComponent>().Value;

                var vectorLengthSqr = transform.position.sqrMagnitude;
                if (vectorLengthSqr > 15 * 15)
                {
                    entity.Get<DestroyComponent>();
                }
            }
        }
    }
}