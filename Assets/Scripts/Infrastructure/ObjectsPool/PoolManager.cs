using UnityEngine;

namespace Infrastructure.ObjectsPool
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolPart[] pools;
        private static GameObject objectsParent;

        [System.Serializable]
        public struct PoolPart {
            public string name;
            public PoolObject prefab;
            public int count;
            public ObjectPooling ferula;
        }

        public static void Initialize(PoolPart[] newPools) {
            pools = newPools;
            objectsParent = new GameObject ();
            objectsParent.name = "Pool";
            for (int i=0; i<pools.Length; i++) {
                if(pools[i].prefab!=null) {
                    pools[i].ferula = new ObjectPooling();
                    pools[i].ferula.Initialize(pools[i].count, pools[i].prefab, objectsParent.transform);
                }
            }
        }

        public static void ClearPool()
        {
            for (int i = 0; i < objectsParent.transform.childCount; i++)
            {
                var poolObject = objectsParent.transform.GetChild(i);
                poolObject.GetComponent<PoolObject>().ReturnToPool();
            }
        }

        public static GameObject GetObject (string name, Vector3 position = default, Quaternion rotation = default) {
            GameObject result = null;
            if (pools != null) {
                for (int i = 0; i < pools.Length; i++) {
                    if (string.Compare (pools [i].name, name) == 0) {
                        result = pools[i].ferula.GetObject ().gameObject;
                        result.transform.position = position;
                        result.transform.rotation = rotation;
                        result.SetActive (true);
                        return result;
                    }
                }
            } 
            return result;
        }

    }
}
