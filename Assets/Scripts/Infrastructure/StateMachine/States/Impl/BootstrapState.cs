
using Infrastructure.ObjectsPool;

namespace Infrastructure.StateMachine.States.Impl
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly PoolSetup _poolSetup;

        public BootstrapState(GameStateMachine stateMachine, PoolSetup poolSetup)
        {
            _stateMachine = stateMachine;
            _poolSetup = poolSetup;
        }

        public void Enter()
        {
            _poolSetup.Construct();
            _stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
            
        }
    }
}