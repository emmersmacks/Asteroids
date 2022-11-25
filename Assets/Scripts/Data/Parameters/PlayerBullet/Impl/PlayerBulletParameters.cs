using UnityEngine;

namespace Data.Parameters.Impl
{
    [CreateAssetMenu(menuName = "Parameters", fileName = nameof(PlayerBulletParameters))]
    public class PlayerBulletParameters : ScriptableObject, IPlayerBulletParameters
    {
        [SerializeField] private LayerMask _damageLayerMask;

        public LayerMask DamageLayerMask => _damageLayerMask;
    }
}