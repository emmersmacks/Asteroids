using System;
using Actions.Components;
using Actions.Systems;
using Game.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Infrastructure
{
    public class EcsLoader : MonoBehaviour
    {
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private EcsWorld _world;

        public void Start()
        {
            _world = new EcsWorld ();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            
            AddInjections();
            AddSystems();
            AddActions();
            
            _updateSystems.ConvertScene();
            _updateSystems.Init ();
            
            _fixedUpdateSystems.ConvertScene();
            _fixedUpdateSystems.Init ();
        }

        private void AddInjections()
        {
            
        }

        private void AddSystems()
        {
            _updateSystems.Add(new DesktopInputSystem());
            _updateSystems.Add(new RotateSystem());
            _updateSystems.Add(new StartMainAttackSystem());
            _fixedUpdateSystems.Add(new MoveSystem());
        }

        private void AddActions()
        {
            _updateSystems.OneFrame<StartMainAttackComponent>();
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