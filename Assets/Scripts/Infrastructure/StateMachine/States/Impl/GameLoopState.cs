using Infrastructure.ObjectsPool;
using UnityEngine.Pool;

namespace Infrastructure.StateMachine.States.Impl
{
    public class GameLoopState : IPayloadState
    {
        private readonly GameStateMachine _stateMachine;
        private EcsLoader _ecsLoader;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter<TPayload>(TPayload payload)
        {
            _ecsLoader = payload as EcsLoader;
        }
        
        public void Exit()
        {
            _ecsLoader.GetComponent<PoolObject>().ReturnToPool();
            PoolManager.ClearPool();
        }
    }
}