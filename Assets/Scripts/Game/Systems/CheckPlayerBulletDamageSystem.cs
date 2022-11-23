using System;
using Game.Components;
using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class CheckPlayerBulletDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<PlayerTagComponent, TransformComponent, BulletComponent, DamageLayerComponent> _bulletsGroup;

        public void Run()
        {
            foreach (var bulletIndex in _bulletsGroup)
            {
                var bulletEntity = _bulletsGroup.GetEntity(bulletIndex);
                
                var bulletTransform = bulletEntity.Get<TransformComponent>().Value;
                var oldBulletPosition = bulletEntity.Get<OldPositionComponent>().Value;

                var bulletPosition = bulletTransform.position;

                var distance = Vector3.Distance(bulletPosition, oldBulletPosition);
                var direction = oldBulletPosition.normalized;
                RaycastHit2D hit = Physics2D.Raycast(bulletPosition, direction, distance);
                    
                if (hit.collider == null)
                    return;

                var damageLayer = bulletEntity.Get<DamageLayerComponent>().Value;
                var layerName = LayerMask.LayerToName(damageLayer);
                
                if (hit.collider.CompareTag(layerName))
                {
                    var uid = hit.collider.gameObject.GetInstanceID();
                    var entity = _world.GetEntityWithUid(uid);
                }
            }
        }
    }
}