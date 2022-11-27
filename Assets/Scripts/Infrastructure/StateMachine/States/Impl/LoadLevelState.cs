
using Data;
using Infrastructure.ObjectsPool;
using UI.Parts.Game.GameScoreCounter;
using UI.Parts.Game.LaserСhargeCounter;
using UI.Screens;
using UI.Screens.Impl;
using UnityEngine;

namespace Infrastructure.StateMachine.States.Impl
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        private GameObject _canvas;

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            var ecsLoaderObject = PoolManager.GetObject(PrefabNames.EcsInstaller);
            var ecsLoader = ecsLoaderObject.GetComponent<EcsLoader>();
            ecsLoader.CreateWorld(_gameStateMachine);
            
            _canvas = PoolManager.GetObject(PrefabNames.GameHud);
            var screen = _canvas.GetComponent<GameScreen>();

            var scoreView = screen.scoreCounterView;
            var scoreController = new ScoreCounterController(scoreView, ecsLoader.World);

            var laserChargeCounterView = screen.LaserChargeCounterView;
            var laserChargeCounterController = new LaserChargeCounterController(laserChargeCounterView, ecsLoader.World);
            
            ecsLoader.StartSystems();
            _gameStateMachine.Enter<GameLoopState, EcsLoader>(ecsLoader);
        }
        
        public void Exit()
        {
            
        }
    }
}