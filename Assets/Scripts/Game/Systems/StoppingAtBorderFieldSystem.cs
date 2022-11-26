using Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class StoppingAtBorderFieldSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<RigidbodyComponent, TransformComponent, PlayerTagComponent> _group;

        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var rigidbody = entity.Get<RigidbodyComponent>().Value;
                var transform = entity.Get<TransformComponent>().Value;

                if (transform.position.y > 5)
                {
                    transform.position = new Vector2(transform.position.x, 5);
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                }
                if (transform.position.y < -5)
                {
                    transform.position = new Vector2(transform.position.x, -5);
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                }
                if (transform.position.x > 9)
                {
                    transform.position = new Vector2(9, transform.position.y);
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                if (transform.position.x < -9)
                {
                    transform.position = new Vector2(-9, transform.position.y);
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
            }
        }
    }
}