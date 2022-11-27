using System;
using Data.Parameters;
using Data.Parameters.Asteroids;
using Data.Parameters.Charges;
using Data.Parameters.Level;
using Data.Parameters.Player;
using Data.Parameters.PlayerBullets;
using Data.Parameters.UFO;
using Esc.Actions.Components;
using Esc.Actions.Systems;
using Esc.Game.Systems;
using Esc.Game.Systems.Asteroids;
using Esc.Game.Systems.Initialize;
using Esc.Game.Systems.Player;
using Esc.Game.Systems.UFO;
using Infrastructure.StateMachine;
using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure
{
    public class EcsLoader : MonoBehaviour
    {
        [SerializeField] private PlayerBulletsParameters _playerBulletsParameters;
        [SerializeField] private BigAsteroidParameters _bigAsteroidParameters;
        [SerializeField] private SmallAsteroidsParameters _smallAsteroidsParameters;
        [SerializeField] private ChargesParameters _chargesParameters;
        [SerializeField] private LevelParameters _levelParameters;
        [SerializeField] private PlayerParameters _playerParameters;
        [SerializeField] private LaserWeaponParameters _laserWeaponParameters;
        [SerializeField] private AsteroidsSpawnParameters _asteroidsSpawnParameters;
        [SerializeField] private UFOSpawnParameters _ufoSpawnParameters;
        [SerializeField] private UFOParameters _ufoParameters;
        
        private EcsSystems _initializeSystems;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        private GameStateMachine _gameStateMachine;
        
        public CustomEcsWorld World;

        public void CreateWorld(GameStateMachine stateMachine)
        {
            World = new CustomEcsWorld();
            _gameStateMachine = stateMachine;
        }
        
        public void StartSystems()
        {
            
            _updateSystems = new EcsSystems(World);
            _fixedUpdateSystems = new EcsSystems(World);
            _initializeSystems = new EcsSystems(World);

            AddSystems();
            AddActions();
            AddInjections();

            _initializeSystems.Init ();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
        }

        private void AddInjections()
        {
            _updateSystems.Inject(_playerBulletsParameters);
            _updateSystems.Inject(_bigAsteroidParameters);
            _updateSystems.Inject(_smallAsteroidsParameters);
            _initializeSystems.Inject(_chargesParameters);
            _updateSystems.Inject(_chargesParameters);
            _updateSystems.Inject(_levelParameters);
            _initializeSystems.Inject(_playerParameters);
            _updateSystems.Inject(_playerParameters);
            _updateSystems.Inject(_laserWeaponParameters);
            _updateSystems.Inject(_asteroidsSpawnParameters);
            _updateSystems.Inject(_ufoSpawnParameters);
            _updateSystems.Inject(_ufoParameters);
            _updateSystems.Inject(_gameStateMachine);
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
            _updateSystems.Add(new CheckPlayerDeadSystem());
            _updateSystems.Add(new StartEndGameSystem());
            _updateSystems.Add(new CheckEnemyDeadSystem());
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

        public void OnDisable()
        {
            if (World != null)
            {
                _updateSystems.Destroy();
                _fixedUpdateSystems.Destroy();
                _initializeSystems.Destroy();
                World.Destroy();
            }
        }
    }
}