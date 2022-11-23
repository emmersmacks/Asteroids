using System;
using Actions.Components;
using Actions.Systems;
using Game.Systems;
using Game.Systems.Initialize;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Infrastructure
{
    public class EcsLoader : MonoBehaviour
    {
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
            
        }

        private void AddSystems()
        {
            _initializeSystems.Add(new GameInitializeSystem());

            _updateSystems.Add(new DesktopInputSystem());
            _updateSystems.Add(new RotateSystem());
            _updateSystems.Add(new StartPlayerMainAttackSystem());
            _updateSystems.Add(new BulletMoveSystem());
            _updateSystems.Add(new CheckPlayerBulletDamageSystem());
            
            _fixedUpdateSystems.Add(new ForceMoveSystem());
        }

        private void AddActions()
        {
            _updateSystems.OneFrame<StartPlayerMainAttackComponent>();
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