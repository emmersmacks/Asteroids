using Esc.Game.Components;
using Esc.Game.Components.Tags;
using Esc.Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class CheckBulletHitSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly EcsFilter<TransformComponent, BulletTagComponent, DamageLayerComponent>.Exclude<DestroyComponent> _bulletsGroup;

        public void Run()
        {
            foreach (var bulletIndex in _bulletsGroup)
            {
                var bulletEntity = _bulletsGroup.GetEntity(bulletIndex);
                
                var bulletTransform = bulletEntity.Get<TransformComponent>().Value;
                var oldBulletPosition = bulletEntity.Get<OldPositionComponent>().Value;
                var bulletPosition = bulletTransform.position;
                
                bulletEntity.Replace(new OldPositionComponent() { Value = bulletPosition });
                
                var distance = Vector3.Distance(bulletPosition, oldBulletPosition);
                var direction = oldBulletPosition - bulletPosition;
                direction = direction.normalized;
                RaycastHit2D hit = Physics2D.Raycast(bulletPosition, direction, distance);
                
                if (hit.collider == null)
                    continue;

                var damageLayer = bulletEntity.Get<DamageLayerComponent>().Value;

                if (IsLayerDamage(damageLayer, hit))
                {
                    var uid = hit.collider.gameObject.GetInstanceID();
                    var entity = _world.GetEntityWithUid(uid);
                    entity.Get<DestroyComponent>();
                    entity.Get<KilledTagComponent>();
                    bulletEntity.Get<DestroyComponent>();
                }
            }
        }

        private static bool IsLayerDamage(LayerMask damageLayer, RaycastHit2D hit)
        {
            return damageLayer == (damageLayer | (1 << hit.collider.gameObject.layer));
        }
    }
}