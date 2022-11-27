using UnityEngine;

namespace Data.Parameters.Asteroids
{
    [CreateAssetMenu(menuName = nameof(SmallAsteroidsParameters), fileName = nameof(SmallAsteroidsParameters))]
    public class SmallAsteroidsParameters : ScriptableObject
    {
        [SerializeField] private int _minSpeed;
        [SerializeField] private int _maxSpeed;
        [SerializeField] private int _costInPoints;

        public int MinSpeed => _minSpeed;
        public int MaxSpeed => _maxSpeed;
        public int CostInPoints => _costInPoints;
    }
}