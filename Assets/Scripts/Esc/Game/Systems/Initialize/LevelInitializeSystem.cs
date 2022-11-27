using Data;
using Game.Extensions;
using Game.Views;
using Infrastructure;
using Leopotam.Ecs;

namespace Game.Systems.Initialize
{
    public class LevelInitializeSystem : IEcsInitSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        public void Init()
        {
            var levelEntity = _world.NewEntity();
            var gameObject = levelEntity.AddPrefab(PrefabNames.Level);
            var levelView = gameObject.GetComponent<LevelView>();
            _world.CreateLevel(levelEntity);

            _world.InitializeAsteroidsSpawner(levelView.AsteroidSpawnPoints);
            _world.InitializeUFOSpawner(levelView.UFOSpawnPoints);

        }
    }
}