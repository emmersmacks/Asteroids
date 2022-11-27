using Data.Parameters.UFO;
using Esc.Game.Components;
using Esc.Game.Components.SpawnPoints;
using Esc.Game.Components.Tags;
using Esc.Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;

namespace Esc.Game.Systems.UFO
{
    public class SpawnUFOSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;

        private readonly UFOSpawnParameters _spawnParameters = null;
        private readonly UFOParameters _ufoParameters = null;
        
        private readonly EcsFilter<SpawnPointsComponent, UFOTagComponent>.Exclude<DelayComponent> _spawnersGroup;
        
        public void Run()
        {
            foreach (var index in _spawnersGroup)
            {
                var spawnerEntity = _spawnersGroup.GetEntity(index);
                
                var spawnPoints = spawnerEntity.Get<SpawnPointsComponent>().Value;

                var random = new System.Random();
                var randomIndex = random.Next(0, spawnPoints.Length);
                var point = spawnPoints[randomIndex];
                
                var transform = point.transform;
            
                _world.CreateUFO(transform.position, _ufoParameters);

                var delayComponent = new DelayComponent() { Value = _spawnParameters.SpawnVo.SpawnDelay };
                spawnerEntity.Replace(delayComponent);
            }
        }
        
        
    }
}