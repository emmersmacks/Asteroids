using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
            new EcsLoader();
        }
    }
}
