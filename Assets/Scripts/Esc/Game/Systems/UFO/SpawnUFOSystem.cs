using Data.Parameters;
using Data.Parameters.Spawners;
using Game.Components;
using Game.Components.SpawnPoints;
using Game.Components.Tags;
using Game.Extensions;
using Infrastructure;
using Leopotam.Ecs;

namespace Game.Systems.UFO
{
    public class SpawnUFOSystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;

        private readonly UFOSpawnParameters _spawnParameters = null;
        private readonly UFOParameters _ufoParameters = null;
        
        private readonly EcsFilter<SpawnPointsComponent, UFOTagComponent>.Exclude<DelayComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                
                var spawnPoints = entity.Get<SpawnPointsComponent>().Value;

                var random = new System.Random();
                var randomIndex = random.Next(0, spawnPoints.Length);
                var point = spawnPoints[randomIndex];
                
                var transform = point.transform;
            
                _world.CreateUFO(transform.position, _ufoParameters);

                var delayComponent = new DelayComponent() { Value = _spawnParameters.SpawnVo.SpawnDelay };
                entity.Replace(delayComponent);
            }
        }
        
        
    }
}