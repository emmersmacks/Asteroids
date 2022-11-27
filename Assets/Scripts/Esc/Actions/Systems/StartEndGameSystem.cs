using Data.Bases;
using Esc.Actions.Components;
using Esc.Game.Components;
using Esc.Game.Components.Tags;
using Infrastructure;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States.Impl;
using Leopotam.Ecs;

namespace Esc.Actions.Systems
{
    public class StartEndGameSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        private readonly GameStateMachine _stateMachine = null;

        private readonly EcsFilter<StartEndGameComponent> _actionGroup = null;
        private readonly EcsFilter<LevelTagComponent> _levelGroup = null;

        public void Run()
        {
            foreach (var actionIndex in _actionGroup)
            {
                var level = _levelGroup.GetEntity(0);
                var score = level.Get<ScoreComponent>().Value;
                var endGameData = new EndGameDataBase();
                endGameData.Score = score;
                _stateMachine.Enter<EndGameState, EndGameDataBase>(endGameData);
            }
        }
    }
}