using UI.Parts.Game.GameScoreCounter;
using UI.Parts.Game.Laser–°hargeCounter;
using UnityEngine;

namespace UI.Screens.Impl
{
    public class GameScreen : MonoBehaviour, IScreen
    {
        public ScoreCounterView scoreCounterView;
        public LaserChargeCounterView LaserChargeCounterView;
    }
}