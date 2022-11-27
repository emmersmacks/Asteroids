using Data;
using Infrastructure.ObjectsPool;
using UI.Menu;
using UI.Screens;
using UnityEngine;

namespace Infrastructure.StateMachine.States.Impl
{
    internal class MenuState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private GameObject _canvas;
        
        public MenuState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _canvas = PoolManager.GetObject(PrefabNames.MenuHud);
            
            var screen = _canvas.GetComponent<MainMenuScreen>();

            var menuView = screen.MenuView;
            var menuController = new MenuController(menuView, _stateMachine);
        }

        public void Exit()
        {
            var poolObject = _canvas.GetComponent<PoolObject>();
            poolObject.ReturnToPool();
        }
    }
}