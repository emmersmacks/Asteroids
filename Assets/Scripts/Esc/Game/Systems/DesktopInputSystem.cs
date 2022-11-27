using Actions.Components;
using Game.Components;
using Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Systems
{
    public class DesktopInputSystem : IEcsRunSystem
    {
        private const int LeftMouseButtonIndex = 0;
        private const int RightMouseButtonIndex = 1;
        
        private readonly CustomEcsWorld _world = null;
        private readonly EcsFilter<DirectionComponent, PlayerTagComponent, UnitComponent> _group = null;

        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);

                var xDirection = Input.GetAxisRaw("Horizontal");
                var yDirection = Input.GetAxisRaw("Vertical");
                var direction = new Vector2(xDirection, yDirection);

                if (Input.GetMouseButtonDown(LeftMouseButtonIndex))
                {
                    _world.NewEntity().Replace(new StartPlayerMainAttackComponent());
                }
                
                if (Input.GetMouseButtonDown(RightMouseButtonIndex))
                {
                    _world.NewEntity().Replace(new StartPlayerLaserAttackComponent());
                }

                var directionComponent = new DirectionComponent() { Value = direction };
                entity.Replace(directionComponent);
            }
        }
    }
}