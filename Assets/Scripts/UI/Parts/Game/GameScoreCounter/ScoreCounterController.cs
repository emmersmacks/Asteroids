using Infrastructure;

namespace UI.Parts.Game.GameScoreCounter
{
    public class ScoreCounterController : Controller<ScoreCounterView>
    {
        public ScoreCounterController(ScoreCounterView view, CustomEcsWorld world) : base(view)
        {
            world.ScoreChange += UpdateCounter;
        }

        private void UpdateCounter(int value)
        {
            _view.CounterText.text = value.ToString();
        }
    }
}