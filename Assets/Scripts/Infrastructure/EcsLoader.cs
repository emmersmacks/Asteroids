using System;
using Actions.Components;
using Actions.Systems;
using Data.Parameters;
using Data.Parameters.PlayerBullet.Impl;
using Game.Systems;
using Game.Systems.Asteroids;
using Game.Systems.Initialize;
using Game.Systems.Player;
using Game.Systems.UFO;
using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure
{
    public class EcsLoader : MonoBehaviour
    {
        [SerializeField] private PlayerBulletParameters _playerBulletParameters;
        
        private EcsSystems _initializeSystems;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        
        public CustomEcsWorld World;

        public void CreateWorld()
        {
            World = new CustomEcsWorld();
        }
        
        public void StartSystems()
        {
            
            _updateSystems = new EcsSystems(World);
            _fixedUpdateSystems = new EcsSystems(World);
            _initializeSystems = new EcsSystems(World);

            AddInjections();
            AddSystems();
            AddActions();

            _initializeSystems.Init ();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
        }

        private void AddInjections()
        {
            _updateSystems.Inject(_playerBulletParameters);
        }

        private void AddSystems()
        {
            _initializeSystems.Add(new PlayerInitializeSystem());
            _initializeSystems.Add(new LevelInitializeSystem());

            _updateSystems.Add(new DesktopInputSystem());
            _updateSystems.Add(new StopAtBorderFieldSystem());
            _updateSystems.Add(new PlayerRotateSystem());
            _updateSystems.Add(new StartPlayerMainAttackSystem());
            _updateSystems.Add(new StartPlayerLaserAttackSystem());
            _updateSystems.Add(new TransformMoveSystem());
            _updateSystems.Add(new CheckBulletHitSystem());
            _updateSystems.Add(new SplitAsteroidSystem());
            _updateSystems.Add(new CountdownSystem());
            _updateSystems.Add(new CountdownToDeletionSystem());
            _updateSystems.Add(new SpawnAsteroidsSystem());
            _updateSystems.Add(new DeletingFlewAwayObjects());
            _updateSystems.Add(new FollowPlayerSystem());
            _updateSystems.Add(new SpawnUFOSystem());
            _updateSystems.Add(new ChargeRecoverySystem());
            _updateSystems.Add(new DestroyEntitySystem());

            _fixedUpdateSystems.Add(new ForceMoveSystem());
        }

        private void AddActions()
        {
            _updateSystems.OneFrame<StartPlayerMainAttackComponent>();
            _updateSystems.OneFrame<StartPlayerLaserAttackComponent>();
        }

        public void Update()
        {
            if(World != null)
                _updateSystems.Run();
        }

        public void FixedUpdate()
        {
            if(World != null)
                _fixedUpdateSystems.Run();
        }

        public void OnDestroy()
        {
            if (World != null)
            {
                _updateSystems.Destroy();
                _fixedUpdateSystems.Destroy();
                World.Destroy();
            }
        }
    }
}