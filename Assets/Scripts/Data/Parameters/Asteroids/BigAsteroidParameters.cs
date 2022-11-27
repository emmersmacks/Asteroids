using UnityEngine;

namespace Data.Parameters.Asteroids
{
    [CreateAssetMenu(menuName = nameof(BigAsteroidParameters), fileName = nameof(BigAsteroidParameters))]
    public class BigAsteroidParameters : ScriptableObject
    {
        [SerializeField] private int _minSpeed;
        [SerializeField] private int _maxSpeed;
        [SerializeField] private int _costInPoints;

        public int MinSpeed => _minSpeed;
        public int MaxSpeed => _maxSpeed;
        public int CostInPoints => _costInPoints;
    }
}