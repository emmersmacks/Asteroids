using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper bootstrapperPrefab;

        private void Awake()
        {
            if (FindObjectOfType<Bootstrapper>() == null)
            {
                Instantiate(bootstrapperPrefab);
            }
        }
    }
}
