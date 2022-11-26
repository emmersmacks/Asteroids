using Game.Extensions;
using Game.Views;
using Leopotam.Ecs;

namespace Game.Systems.Initialize
{
    public class LevelInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            var gameObject = entity.AddPrefab("Level");
            var levelView = gameObject.GetComponent<LevelView>();
            _world.CreateLevel();

            _world.InitializeAsteroidsSpawner(levelView.AsteroidSpawnPoints);
            _world.InitializeUFOSpawner(levelView.UFOSpawnPoints);

        }
    }
}