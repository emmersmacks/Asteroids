using UnityEngine;

namespace Data.Parameters.Spawners
{
    [CreateAssetMenu(menuName = nameof(AsteroidsSpawnParameters), fileName = nameof(AsteroidsSpawnParameters))]
    public class AsteroidsSpawnParameters : ScriptableObject
    {
        [SerializeField] private SpawnVo _spawnVo;

        public SpawnVo SpawnVo => _spawnVo;
    }
}