using UnityEngine;

namespace Data.Parameters.Level
{
    [CreateAssetMenu(menuName = nameof(LevelParameters), fileName = nameof(LevelParameters))]
    public class LevelParameters : ScriptableObject
    {
        [SerializeField] private float _clearFlewObjectsDistanse;
        
        [SerializeField] private float _lowerLevelLimitCoord = -5;
        [SerializeField] private float _upperLevelLimitCoord = 5;
        [SerializeField] private float _leftLevelLimitCoord = -9;
        [SerializeField] private float _rightLevelLimitCoord = 9;

        public float ClearFlewObjectsDistanse => _clearFlewObjectsDistanse;
        
        public float LowerLevelLimitCoord => _lowerLevelLimitCoord;
        public float UpperLevelLimitCoord => _upperLevelLimitCoord;
        public float LeftLevelLimitCoord => _leftLevelLimitCoord;
        public float RightLevelLimitCoord => _rightLevelLimitCoord;
    }
}