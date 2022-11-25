using Game.Components;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;

namespace Game.Systems
{
    public class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DestroyComponent, TransformComponent> _group = null;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var transform = entity.Get<TransformComponent>().Value;

                var poolObject = transform.GetComponent<PoolObject>();
                poolObject.ReturnToPool();
                
                entity.Destroy();
            }
        }
    }
}