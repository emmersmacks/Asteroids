using UnityEngine;

namespace Infrastructure.ObjectsPool
{
    public class PoolSetup : MonoBehaviour
    {
        [SerializeField] private PoolManager.PoolPart[] pools;

        public void Construct()
        {
            Initialize();
        }
        
        void OnValidate() 
        {
            for (int i = 0; i < pools.Length; i++) {
                pools[i].name = pools[i].prefab.name;
            }
        }

        void Initialize () 
        {
            PoolManager.Initialize(pools);
        }
    }
}
