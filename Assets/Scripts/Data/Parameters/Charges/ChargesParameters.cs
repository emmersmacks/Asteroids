using UnityEngine;

namespace Data.Parameters.PlayerBullet.Impl.Charges
{
    [CreateAssetMenu(menuName = nameof(ChargesParameters), fileName = nameof(ChargesParameters))]
    public class ChargesParameters : ScriptableObject
    {
        [SerializeField] private float _recoveryTime;
        [SerializeField] private int _chargesRestored;
        [SerializeField] private int _maxCharges;
        
        public float RecoveryTime => _recoveryTime;
        public int ChargesRestored => _chargesRestored;
        public int MaxCharges => _maxCharges;
    }
}