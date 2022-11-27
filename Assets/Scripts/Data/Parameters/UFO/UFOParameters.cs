using UnityEngine;

namespace Data.Parameters
{
    [CreateAssetMenu(menuName = nameof(UFOParameters), fileName = nameof(UFOParameters))]
    public class UFOParameters : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _costInPoints;

        public float Speed => _speed;
        public int CostInPoints => _costInPoints;
    }
}