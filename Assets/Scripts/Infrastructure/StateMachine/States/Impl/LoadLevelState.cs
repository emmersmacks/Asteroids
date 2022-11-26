
using Infrastructure.ObjectsPool;
using UI.Game.ScoreCounter;
using UI.Menu;
using UI.Parts.Game.LaserСhargeCounter;
using UI.Screens;
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
            var ecsLoaderObject = PoolManager.GetObject("EcsInstaller");
            var ecsLoader = ecsLoaderObject.GetComponent<EcsLoader>();
            ecsLoader.CreateWorld();
            
            _canvas = PoolManager.GetObject("GameHud");
            var screen = _canvas.GetComponent<GameScreen>();

            var scoreView = screen.ScoreCounterView;
            var scoreController = new ScoreCounterController(scoreView, ecsLoader.World);

            var laserChargeCounterView = screen.LaserChargeCounterView;
            var laserChargeCounterController = new LaserChargeCounterController(laserChargeCounterView, ecsLoader.World);
            
            ecsLoader.StartSystems();
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        public void Exit()
        {
            
        }
    }
}