using Game.Components;
using Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class FollowPlayerSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly EcsFilter<FollowPlayerComponent, TransformComponent> _followersGroup = null;
        private readonly EcsFilter<PlayerTagComponent, UnitComponent, TransformComponent> _playersGroup = null;
        
        public void Run()
        {
            foreach (var followerIndex in _followersGroup)
            {
                foreach (var playerIndex in _playersGroup)
                {
                    var playerEntity = _playersGroup.GetEntity(playerIndex);
                    var playerTransform = playerEntity.Get<TransformComponent>().Value;
                    var playerPosition = playerTransform.position;
                    
                    var followerEntity = _followersGroup.GetEntity(followerIndex);
                    var followerTransform = followerEntity.Get<TransformComponent>().Value;
                    var followerPosition = followerTransform.position;

                    var moveDirection = playerPosition - followerPosition;
                    var lookAngle = GetLookAngle(followerTransform, playerPosition);
                    followerTransform.eulerAngles = lookAngle;
                }
            }
        }
        
        private Vector3 GetLookAngle(Transform transform, Vector3 direction, Vector3? eye = null)
        {
            float signedAngle = Vector2.SignedAngle(eye ?? transform.up, direction - transform.position);

            //Sorry for the magic numbers, but I do not know what this terrible number means
            //Without this number, this thing doesn't work
            //And I do not know why :)
            if (Mathf.Abs(signedAngle) >= 1e-3f)
            {
                var angles = transform.eulerAngles;
                angles.z += signedAngle;
                return angles;
            }

            return transform.eulerAngles;
        }
    }
}