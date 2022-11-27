using UnityEngine;

namespace Data.Parameters.Player
{
    [CreateAssetMenu(menuName = nameof(PlayerParameters), fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _deadDistance;

        public float DeadDistance => _deadDistance;
        public float Speed => _speed;
        public float RotateSpeed => _rotateSpeed;
    }
}