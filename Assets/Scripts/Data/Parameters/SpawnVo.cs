using System;
using UnityEngine;

namespace Data.Parameters.Spawners
{
    [Serializable]
    public class SpawnVo
    {
        [SerializeField] private float _spawnDelay;

        public float SpawnDelay => _spawnDelay;
    }
}