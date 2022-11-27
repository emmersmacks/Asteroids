using Esc.Game.Components;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems.Player
{
    public class PlayerRotateSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;

        public void Run()
        {
            var entity = _world.Player;
                
            var direction = entity.Get<DirectionComponent>().Value;
            var transform = entity.Get<TransformComponent>().Value;
            var rotateSpeed = entity.Get<RotateSpeedComponent>().Value;

            var zRotateAngle = direction.x * rotateSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, -zRotateAngle));
        }
    }
}