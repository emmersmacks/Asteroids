using Data;
using Data.Bases;
using Infrastructure.ObjectsPool;
using UI.Game.ScoreCounter;
using UI.Parts.Game.EndGame;
using UI.Parts.Game.ScoreResult;
using UI.Screens;
using UnityEngine;

namespace Infrastructure.StateMachine.States.Impl
{
    public class EndGameState : IPayloadState
    {
        private readonly GameStateMachine _stateMachine;
        private GameObject _screenObject;

        public EndGameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter<TPayload>(TPayload payload)
        {
            var endGameDataBase = payload as EndGameDataBase;

            _screenObject = PoolManager.GetObject(PrefabNames.EndGameHud);
            var screen = _screenObject.GetComponent<EndGameScreen>();
                
            var endGameController = new EndGameController(screen.EndGameView, _stateMachine);
            
            var scoreController = new ScoreResultController(screen.gameScoreCounterView);
            scoreController.SetScore(endGameDataBase.Score);
        }

        public void Exit()
        {
            _screenObject.GetComponent<PoolObject>().ReturnToPool();
        }
    }
}