using System;
using Actions.Components;
using Actions.Systems;
using Data.Parameters;
using Data.Parameters.Impl;
using Game.Systems;
using Game.Systems.Initialize;
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
        private EcsWorld _world;
        
        public void Construct()
        {
            _world = new EcsWorld ();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _initializeSystems = new EcsSystems(_world);
            
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
            _initializeSystems.Add(new GameInitializeSystem());

            _updateSystems.Add(new DesktopInputSystem());
            _updateSystems.Add(new StoppingAtBorderFieldSystem());
            _updateSystems.Add(new RotateSystem());
            _updateSystems.Add(new StartPlayerMainAttackSystem());
            _updateSystems.Add(new StartPlayerLaserAttackSystem());
            _updateSystems.Add(new TransformMoveSystem());
            _updateSystems.Add(new CheckBulletDamageSystem());
            _updateSystems.Add(new SplitAsteroidSystem());
            _updateSystems.Add(new DelayCountdownSystem());
            _updateSystems.Add(new DestroyDelayCountdownSystem());
            _updateSystems.Add(new SpawnAsteroidsSystem());
            _updateSystems.Add(new ClearObjectsOnDistanceSystem());
            _updateSystems.Add(new FollowPlayerSystem());
            _updateSystems.Add(new SpawnUFOSystem());
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
            _updateSystems.Run();
        }

        public void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        public void OnDestroy()
        {
            _updateSystems.Destroy();
            _fixedUpdateSystems.Destroy();
            _world.Destroy();
        }
    }
}