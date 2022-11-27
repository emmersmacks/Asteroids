using UnityEngine;

namespace Data.Parameters.PlayerBullets
{
    [CreateAssetMenu(menuName = nameof(PlayerBulletsParameters), fileName = nameof(PlayerBulletsParameters))]
    public class PlayerBulletsParameters : ScriptableObject
    {
        [SerializeField] private LayerMask _damageLayerMask;
        [SerializeField] private float _speed;

        public LayerMask DamageLayerMask => _damageLayerMask;
        public float Speed => _speed;
    }
}