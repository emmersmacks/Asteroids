using UnityEngine;

namespace Data.Parameters.Spawners
{
    [CreateAssetMenu(menuName = nameof(UFOSpawnParameters), fileName = nameof(UFOSpawnParameters))]
    public class UFOSpawnParameters : ScriptableObject
    {
        [SerializeField] private SpawnVo _spawnVo;

        public SpawnVo SpawnVo => _spawnVo;
    }
}