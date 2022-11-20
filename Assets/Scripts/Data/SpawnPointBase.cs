using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct SpawnPointBase
    {
        public Transform Point;
        [HideInInspector] public bool IsSpawned;
    }
}