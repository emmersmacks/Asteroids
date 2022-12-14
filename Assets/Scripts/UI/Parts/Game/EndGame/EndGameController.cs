using Data.Bases;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States.Impl;
using UnityEngine;

namespace UI.Parts.Game.EndGame
{
    public class EndGameController : Controller<EndGameView>
    {
        private readonly GameStateMachine _stateMachine;

        public EndGameController(EndGameView view, GameStateMachine stateMachine) : base(view)
        {
            _stateMachine = stateMachine;

            _view.ReloadGame.onClick.AddListener(ReloadGame);
            _view.ExitGame.onClick.AddListener(ExitGame);
        }

        private void ReloadGame()
        {
            _stateMachine.Enter<LoadLevelState>();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}