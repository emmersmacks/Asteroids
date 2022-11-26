using Infrastructure;
using Leopotam.Ecs;

namespace UI.Game.ScoreCounter
{
    public class ScoreCounterController
    {
        private readonly ScoreCounterView _view;

        public ScoreCounterController(ScoreCounterView view, CustomEcsWorld world)
        {
            _view = view;
            world.ScoreChange += UpdateCounter;
        }

        private void UpdateCounter(int value)
        {
            _view.CounterText.text = value.ToString();
        }
    }
}