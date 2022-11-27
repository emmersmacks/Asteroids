using System;
using System.Runtime.CompilerServices;
using Esc.Game.Components;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Extensions
{
    public static class EntityExtensions
    {
        public static GameObject AddPrefab(this EcsEntity entity, string name, Vector3 position = default, Quaternion rotation = default)
        {
            var gameObject = PoolManager.GetObject(name, position, rotation);
            var objectUid = gameObject.GetInstanceID();
            entity.ReplaceComponent(new UidComponent() { Value = objectUid });
            return gameObject;
        }
        
        public static void AddParent(this EcsEntity entity, EcsEntity parentEntity)
        {
            var transform = entity.Get<TransformComponent>().Value;
            var parentTransform = parentEntity.Get<TransformComponent>().Value;
            
            transform.SetParent(parentTransform);
        }
        
        public static EcsEntity ReplaceComponent<T> (in this EcsEntity entity, in T item) where T : struct
        {
            if (entity.Has<ViewComponent>())
            {
                var view = entity.Get<ViewComponent>().Value;
                if(view.EcsEvents.ContainsKey(item.GetType()))
                    view.SendAddEvent(item);
            }

            return entity.Replace(item);
        }
    }
}