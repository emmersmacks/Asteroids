using Esc.Actions.Components;
using Esc.Game.Components;
using Esc.Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class DesktopInputSystem : IEcsRunSystem
    {
        private const int LeftMouseButtonIndex = 0;
        private const int RightMouseButtonIndex = 1;
        
        private readonly CustomEcsWorld _world = null;

        public void Run()
        {
            var playerEntity = _world.Player;

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
            playerEntity.Replace(directionComponent);
        }
    }
}