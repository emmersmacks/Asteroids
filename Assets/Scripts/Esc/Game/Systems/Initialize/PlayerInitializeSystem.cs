using Data.Parameters.Charges;
using Data.Parameters.Player;
using Esc.Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Systems.Initialize
{
    public class PlayerInitializeSystem : IEcsInitSystem
    {
        private readonly CustomEcsWorld _world = null;

        private readonly PlayerParameters _playerParameters = null;
        private readonly ChargesParameters _chargesParameters = null;
        
        public void Init()
        {
            var player = _world.CreatePlayer(Vector3.zero, _playerParameters);
            _world.CreateMainWeapon(Vector3.zero, player);
            _world.CreateLaserWeapon(Vector3.zero, player, _chargesParameters);
        }
    }
}

