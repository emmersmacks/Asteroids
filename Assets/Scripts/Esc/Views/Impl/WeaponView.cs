using System;
using Esc.Actions.Components;
using Esc.Game.Components;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Esc.Views
{
    public class WeaponView : ObjectView
    {
        [SerializeField] private AudioSource _shootSound;
        
        private OnComponentAdded ShootAdded;
        
        public override void Link(ref EcsEntity entity)
        {
            base.Link(ref entity);
            EcsEvents.Add(typeof(ShootComponent), ShootAdded = OnShootAdded);
        }
        
        protected void OnShootAdded(ValueType component)
        {
            Debug.Log("DDD");
            _shootSound.Play();
        }

        private void OnDisable()
        {
            _shootSound.Stop();
        }
    }
}