using System;
using Infrastructure.ObjectsPool;
using UnityEngine;

namespace Data.Installers.Impl
{
    [CreateAssetMenu(menuName = "PrefabBase", fileName = nameof(PrefabsBase))]
    [Serializable]
    public class PrefabsBase : ScriptableObject, IPrefabsBase
    {
        public PoolManager.PoolPart[] Pools;
        
        void OnValidate() 
        {
            for (int i = 0; i < Pools.Length; i++) {
                Pools[i].name = Pools[i].prefab.name;
            }
        }
    }
}