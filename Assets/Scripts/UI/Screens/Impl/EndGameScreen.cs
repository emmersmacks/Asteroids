using UI.Parts.Game.EndGame;
using UI.Parts.Game.ScoreResult;
using UnityEngine;

namespace UI.Screens
{
    public class EndGameScreen : MonoBehaviour, IScreen
    {
        public EndGameView EndGameView;
        public ScoreResultView gameScoreCounterView;
    }
}