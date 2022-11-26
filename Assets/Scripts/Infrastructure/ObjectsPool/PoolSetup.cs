using System.Collections.Generic;
using Data.Installers;
using Data.Installers.Impl;
using UnityEngine;

namespace Infrastructure.ObjectsPool
{
    public class PoolSetup : MonoBehaviour
    {
        [SerializeField] private PrefabsBase[] prefabsBases;

        private PoolManager.PoolPart[] _pools;
        
        public void Construct()
        {
            Initialize();
        }

        void Initialize ()
        {
            var poolsList = new List<PoolManager.PoolPart>();
            foreach (var prefBase in prefabsBases)
            {
                foreach (var basePools in prefBase.Pools)
                {
                    poolsList.Add(basePools);
                }
            }

            _pools = poolsList.ToArray();
            PoolManager.Initialize(_pools);
        }
    }
}
