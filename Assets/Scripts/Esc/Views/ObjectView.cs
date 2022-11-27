using System;
using System.Collections.Generic;
using Esc.Actions.Components;
using Esc.Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Views
{
    public class ObjectView : MonoBehaviour
    {
        public Dictionary<Type, OnComponentAdded> EcsEvents;
        protected EcsEntity _linkedEntity;

        public delegate void OnComponentAdded(ValueType component);

        public virtual void Link(ref EcsEntity entity)
        {
            _linkedEntity = entity;
            EcsEvents = new Dictionary<Type, OnComponentAdded>();
        }

        public void SendAddEvent(ValueType component)
        {
            EcsEvents[component.GetType()].Invoke(component);
        }
    }
}