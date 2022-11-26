using System;
using Leopotam.Ecs;

namespace Infrastructure
{
    public class CustomEcsWorld : EcsWorld
    {
        public Action<int> LaserChargeChange;
        public Action<int> ScoreChange;
    }
}