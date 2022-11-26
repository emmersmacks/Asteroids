namespace Infrastructure.StateMachine.States
{
    public interface IPayloadState : IExitableState
    {
        void Enter<TPayload>(TPayload payload);
    }
}