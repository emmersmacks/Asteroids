using UI.Parts.Game.GameScoreCounter;
using UI.Parts.Game.LaserСhargeCounter;
using UnityEngine;

namespace UI.Screens
{
    public class GameScreen : MonoBehaviour, IScreen
    {
        public ScoreCounterView scoreCounterView;
        public LaserChargeCounterView LaserChargeCounterView;
    }
}