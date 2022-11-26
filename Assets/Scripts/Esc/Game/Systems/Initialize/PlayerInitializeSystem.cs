using Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems.Initialize
{
    public class PlayerInitializeSystem : IEcsInitSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        public void Init()
        {
            var player = _world.CreatePlayer(Vector3.zero);
            _world.CreateMainWeapon(Vector3.zero, player);
            _world.CreateLaserWeapon(Vector3.zero, player);
        }
    }
}

