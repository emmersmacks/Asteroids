using Esc.Game.Components.Tags;
using Game.Components;
using Game.Components.Tags;
using Infrastructure;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;

namespace Game.Systems
{
    public class CheckEnemyDeadSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        
        private readonly EcsFilter<DestroyComponent, EnemyTagComponent, CostInPointsComponent, KilledTagComponent> _enemyGroup = null;
        private readonly EcsFilter<LevelTagComponent>.Exclude<DestroyComponent> _levelGroup = null;
        
        public void Run()
        {
            foreach (var index in _enemyGroup)
            {
                var level = _levelGroup.GetEntity(0);
                var score = level.Get<ScoreComponent>().Value;

                var enemy = _enemyGroup.GetEntity(index);
                var cost = enemy.Get<CostInPointsComponent>().Value;
                
                var newScore = score + cost;
                var scoreComponent = new ScoreComponent() { Value = newScore };
                level.Replace(scoreComponent);
                _world.ScoreChange?.Invoke(newScore);

                var enemyTransform = enemy.Get<TransformComponent>().Value;
                PoolManager.GetObject("BoomVfx", enemyTransform.position);
            }
        }
    }
}