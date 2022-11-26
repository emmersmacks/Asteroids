using Game.Components;
using Game.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class SpawnUFOSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<UFOSpawnPointsComponent>.Exclude<DelayComponent> _group;
        
        public void Run()
        {
            foreach (var index in _group)
            {
                var entity = _group.GetEntity(index);
                var points = entity.Get<UFOSpawnPointsComponent>().Value;

                var random = new System.Random();
                var randomIndex = random.Next(0, points.Length);
                var randomPoint = points[randomIndex];

                var pointPosition = randomPoint.transform.position;
                _world.CreateUFO(pointPosition);

                var delayComponent = new DelayComponent() { Value = 10 };
                entity.Replace(delayComponent);
            }
        }
        
        
    }
}