using Data.Parameters.Level;
using Esc.Game.Components;
using Esc.Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems
{
    public class StopAtBorderFieldSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly LevelParameters _levelParameters = null;

        public void Run()
        {
            var playerEntity = _world.Player;
                
            var rigidbody = playerEntity.Get<RigidbodyComponent>().Value;
            var transform = playerEntity.Get<TransformComponent>().Value;

            if (transform.position.y > _levelParameters.UpperLevelLimitCoord)
            {
                transform.position = new Vector2(transform.position.x, _levelParameters.UpperLevelLimitCoord);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
            if (transform.position.y < _levelParameters.LowerLevelLimitCoord)
            {
                transform.position = new Vector2(transform.position.x, _levelParameters.LowerLevelLimitCoord);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
            if (transform.position.x > _levelParameters.RightLevelLimitCoord)
            {
                transform.position = new Vector2(_levelParameters.RightLevelLimitCoord, transform.position.y);
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
            if (transform.position.x < _levelParameters.LeftLevelLimitCoord)
            {
                transform.position = new Vector2(_levelParameters.LeftLevelLimitCoord, transform.position.y);
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }
    }
}