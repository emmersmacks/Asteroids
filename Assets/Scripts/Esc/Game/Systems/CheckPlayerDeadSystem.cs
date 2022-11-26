using Actions.Components;
using Esc.Game.Components.Tags;
using Game.Components;
using Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;

namespace Game.Systems
{
    public class CheckPlayerDeadSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly EcsFilter<EnemyTagComponent, UnitComponent> _enemyGroup = null;
        private readonly EcsFilter<PlayerTagComponent, UnitComponent> _playerGroup = null;
        
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
                if (vectorLengthSqr < 1 * 1)
                {
                    _world.NewEntity().Get<StartEndGameComponent>();
                }
            }
        }
    }
}