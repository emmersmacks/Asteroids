using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States.Impl;
using UnityEngine;

namespace UI.Parts.Game.Menu
{
    public class MenuController
    {
        private readonly MenuView _view;
        private readonly GameStateMachine _stateMachine;

        public MenuController(MenuView view, GameStateMachine stateMachine)
        {
            _view = view;
            _stateMachine = stateMachine;
            _view.StartButton.onClick.AddListener(StartGame);
            _view.ExitButton.onClick.AddListener(ExitGame);
        }

        private void StartGame()
        {
            _stateMachine.Enter<LoadLevelState>();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}