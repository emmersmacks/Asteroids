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
        
        private readonly EcsFilter<TransformComponent>.Exclude<DestroyComponent> _flewAwayGroup = null;
        
        public void Run()
        {
            foreach (var index in _flewAwayGroup)
            {
                var flewAwayEntity = _flewAwayGroup.GetEntity(index);

                var transform = flewAwayEntity.Get<TransformComponent>().Value;

                var vectorLengthSqr = transform.position.sqrMagnitude;
                var clearDistance = _levelParameters.ClearFlewObjectsDistanse;
                var avalibleDistanceSqr = clearDistance * clearDistance; 
                if (vectorLengthSqr > avalibleDistanceSqr)
                {
                    flewAwayEntity.Get<DestroyComponent>();
                }
            }
        }
    }
}