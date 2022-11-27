using UnityEngine;

namespace Data.Parameters.PlayerBullet.Impl
{
    [CreateAssetMenu(menuName = nameof(LaserWeaponParameters), fileName = nameof(LaserWeaponParameters))]
    public class LaserWeaponParameters : ScriptableObject
    {
        [SerializeField] private float _hitDistance;
        [SerializeField] private float _destroyDelay;
        [SerializeField] private int _chargeСost;
        [SerializeField] private LayerMask _damageLayerMask;

        public LayerMask DamageLayerMask => _damageLayerMask;
        public float HitDistance => _hitDistance;
        public float DestroyDelay => _destroyDelay;
        public int ChargeСost => _chargeСost;
    }
}