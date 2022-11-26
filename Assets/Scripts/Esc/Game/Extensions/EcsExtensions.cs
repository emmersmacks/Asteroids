using System;
using Game.Components;
using Infrastructure.ObjectsPool;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Extensions
{
    public static class EcsExtensions
    {
        public static EcsEntity GetEntityWithUid(this EcsWorld world, int Uid)
        {
            EcsEntity[] entites = null;
            world.GetAllEntities(ref entites);
            foreach (var entity in entites)
            {
                var uidComponent = entity.Get<UidComponent>();
                if (uidComponent.Value == Uid)
                {
                    return entity;
                }
            }

            throw new Exception("Uid not found!");
        }
    }
}