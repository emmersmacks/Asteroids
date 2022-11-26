namespace Infrastructure.StateMachine.States.Impl
{
    public class EndGameState : IPayloadState
    {
        private readonly GameStateMachine _stateMachine;

        public EndGameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter<TPayload>(TPayload payload)
        {
        }

        public void Exit()
        {
            
        }
    }
}