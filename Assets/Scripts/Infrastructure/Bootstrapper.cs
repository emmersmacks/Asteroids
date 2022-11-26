using Infrastructure.ObjectsPool;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States.Impl;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public PoolSetup PoolSetup;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            var stateMachine = new GameStateMachine(PoolSetup);
            stateMachine.Enter<BootstrapState>();
        }
    }
}
