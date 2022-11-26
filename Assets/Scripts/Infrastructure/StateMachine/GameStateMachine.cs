using System;
using System.Collections.Generic;
using Infrastructure.ObjectsPool;
using Infrastructure.StateMachine.States;
using Infrastructure.StateMachine.States.Impl;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(PoolSetup poolSetup)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                { typeof(BootstrapState), new BootstrapState(this, poolSetup) },
                { typeof(MenuState), new MenuState(this) },
                { typeof(LoadLevelState), new LoadLevelState(this)},
                { typeof(GameLoopState), new GameLoopState(this)},
                { typeof(EndGameState), new EndGameState(this)},
            };
        }

        public void Enter<TState>() where TState : IState
        {
            Exit<TState>();
            var state = _states[typeof(TState)] as IState;
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState
        {
            Exit<TState>();
            var state = _states[typeof(TState)] as IPayloadState;
            state.Enter<TPayload>(payload);
        }

        public void Exit<TState>() where TState : IExitableState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(TState)];
        }
    }
}