using System;
using UnityEngine;

namespace Data.Bases
{
    [Serializable]
    public struct BoolSpawnPointBase
    {
        public Transform Point;
        [HideInInspector] public bool IsSpawned;
    }
}