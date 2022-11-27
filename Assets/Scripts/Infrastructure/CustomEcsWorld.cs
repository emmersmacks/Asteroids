using System;
using System.Collections.Generic;
using Leopotam.Ecs;

namespace Infrastructure
{
    public class CustomEcsWorld : EcsWorld
    {
        public Action<int> LaserChargeChange;
        public Action<int> ScoreChange;

        public EcsEntity Player;
        public EcsEntity Level;

        public void SetUnique(ref EcsEntity entityField, ref EcsEntity entityValue)
        {
            entityField = entityValue;
        }
        
    }
}