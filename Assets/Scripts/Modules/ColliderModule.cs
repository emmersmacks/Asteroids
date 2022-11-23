using System;
using UnityEngine;

namespace Modules
{
    public class ColliderModule : MonoBehaviour
    {
        public Action<Collider> OnEnterTrigger;

        private void OnTriggerEnter(Collider other)
        {
            OnEnterTrigger?.Invoke(other);
        }
  }
}