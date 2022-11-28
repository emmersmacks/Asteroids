using Esc.Game.Components;
using Esc.Game.Components.Tags;
using Esc.Game.Extensions;
using Infrastructure;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class CheckEnemyDeadSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly EcsFilter<DestroyComponent, EnemyTagComponent, CostInPointsComponent, KilledTagComponent> _enemyGroup = null;

        public void Run()
        {
            foreach (var index in _enemyGroup)
            {
                var level = _world.Level;
                var score = level.Get<ScoreComponent>().Value;

                var enemy = _enemyGroup.GetEntity(index);
                var cost = enemy.Get<CostInPointsComponent>().Value;
                
                var newScore = score + cost;
                var scoreComponent = new ScoreComponent() { Value = newScore };
                level.Replace(scoreComponent);
                _world.ScoreChange?.Invoke(newScore);

                var enemyTransform = enemy.Get<TransformComponent>().Value;
                _world.CreateBoomVfx(enemyTransform.position);
            }
        }
    }
}