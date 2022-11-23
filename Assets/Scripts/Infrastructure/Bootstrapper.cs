using Infrastructure.ObjectsPool;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public EcsLoader EcsLoader;
        public PoolSetup PoolSetup;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            PoolSetup.Construct();
            EcsLoader.Construct();
        }
    }
}
