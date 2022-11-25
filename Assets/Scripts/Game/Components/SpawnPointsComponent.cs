using System;
using Data;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public struct SpawnPointsComponent
    {
        public BulletSpawnPointBase[] Value;
    }
}