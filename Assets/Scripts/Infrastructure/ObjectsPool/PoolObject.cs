using UnityEngine;

namespace Infrastructure.ObjectsPool
{
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool () {
            gameObject.SetActive (false);
        }
    }
}
