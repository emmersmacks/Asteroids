using Data.Parameters.Player;
using Esc.Actions.Components;
using Esc.Game.Components;
using Esc.Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;

namespace Esc.Game.Systems
{
    public class CheckPlayerDeadSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly PlayerParameters _playerParameters = null;

        
        private readonly EcsFilter<EnemyTagComponent, UnitComponent>.Exclude<DestroyComponent> _enemyGroup = null;
        private readonly EcsFilter<PlayerTagComponent, UnitComponent>.Exclude<DestroyComponent> _playerGroup = null;
        
        public void Run()
        {
            var playerEntity = _playerGroup.GetEntity(0);

            foreach (var enemyIndex in _enemyGroup)
            {
                var enemyEntity = _enemyGroup.GetEntity(enemyIndex);
                
                var enemyTransform = enemyEntity.Get<TransformComponent>().Value;
                var playerTransform = playerEntity.Get<TransformComponent>().Value;

                var direction = enemyTransform.position - playerTransform.position;

                var vectorLengthSqr = direction.sqrMagnitude;
                var availableDistanceSqr = _playerParameters.DeadDistance * _playerParameters.DeadDistance;
                if (vectorLengthSqr < availableDistanceSqr)
                {
                    _world.NewEntity().Get<StartEndGameComponent>();
                }
            }
        }
    }
}