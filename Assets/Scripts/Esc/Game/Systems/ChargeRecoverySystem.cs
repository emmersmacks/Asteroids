using Game.Components;
using Game.Components.Tags;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems
{
    public class ChargeRecoverySystem : IEcsRunSystem
    {
        private readonly CustomEcsWorld _world = null;
        
        private readonly EcsFilter<WeaponTagComponent, ChargesComponent, MaxChargesComponent>.Exclude<DestroyComponent, DelayComponent> _bulletsGroup;
        
        public void Run()
        {
            foreach (var index in _bulletsGroup)
            {
                var entity = _bulletsGroup.GetEntity(index);

                var maxCharges = entity.Get<MaxChargesComponent>().Value;
                var currentCharges = entity.Get<ChargesComponent>().Value;

                if (currentCharges >= maxCharges)
                    continue;

                var newChargesNumber = currentCharges + 1;
                var chargesComponent = new ChargesComponent() { Value = newChargesNumber };
                _world.LaserChargeChange(newChargesNumber);
                entity.Replace(chargesComponent);
                
                var delayComponent = new DelayComponent() { Value = 2 };
                entity.Replace(delayComponent);
            }
        }
    }
}