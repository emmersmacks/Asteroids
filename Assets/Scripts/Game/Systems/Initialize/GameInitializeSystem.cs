using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems.Initialize
{
    public class GameInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            _world.CreatePlayer(Vector3.zero);
            _world.CreateMainWeapon(Vector3.zero);
        }
    }
}

