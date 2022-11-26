using Actions.Components;
using Game.Components;
using Game.Components.Tags;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Systems
{
    public class DesktopInputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, PlayerTagComponent, UnitComponent> _group = null;

        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);

                var xDirection = Input.GetAxisRaw("Horizontal");
                var yDirection = Input.GetAxisRaw("Vertical");
                var direction = new Vector2(xDirection, yDirection);

                if (Input.GetMouseButtonDown(0))
                {
                    _world.NewEntity().Replace(new StartPlayerMainAttackComponent());
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    _world.NewEntity().Replace(new StartPlayerLaserAttackComponent());
                }

                var directionComponent = new DirectionComponent() { Value = direction };
                entity.Replace(directionComponent);
            }
        }
    }
}