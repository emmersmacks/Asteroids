using Esc.Game.Components;
using Infrastructure;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;

namespace Esc.Game.Systems
{
    public class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DestroyComponent, TransformComponent> _entityGroup = null;
        
        public void Run()
        {
            foreach (var index in _entityGroup)
            {
                var entity = _entityGroup.GetEntity(index);
                
                var transform = entity.Get<TransformComponent>().Value;

                var poolObject = transform.GetComponent<PoolObject>();
                poolObject.ReturnToPool();
                
                entity.Destroy();
            }
        }
    }
}