using Data.Parameters.Level;
using Esc.Game.Components;
using Infrastructure;
using Leopotam.Ecs;

namespace Esc.Game.Systems
{
    public class DeletingFlewAwayObjects : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;

        private readonly LevelParameters _levelParameters = null;
        
        private readonly EcsFilter<TransformComponent>.Exclude<DestroyComponent> _group = null;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);

                var transform = entity.Get<TransformComponent>().Value;

                var vectorLengthSqr = transform.position.sqrMagnitude;
                var clearDistance = _levelParameters.ClearFlewObjectsDistanse;
                var avalibleDistanceSqr = clearDistance * clearDistance; 
                if (vectorLengthSqr > avalibleDistanceSqr)
                {
                    entity.Get<DestroyComponent>();
                }
            }
        }
    }
}