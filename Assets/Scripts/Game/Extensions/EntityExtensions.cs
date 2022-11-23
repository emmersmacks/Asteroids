using Game.Components;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Extensions
{
    public static class EntityExtensions
    {
        public static GameObject AddPrefab(this EcsEntity entity, string name, Vector3 position = default, Quaternion rotation = default)
        {
            var gameObject = PoolManager.GetObject(name, position, rotation);
            var objectUid = gameObject.GetInstanceID();
            entity.Replace(new UidComponent() { Value = objectUid });
            return gameObject;
        }
    }
}