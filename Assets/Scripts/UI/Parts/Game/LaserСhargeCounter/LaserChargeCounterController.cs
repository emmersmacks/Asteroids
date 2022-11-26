using Infrastructure;
using Leopotam.Ecs;

namespace UI.Parts.Game.LaserСhargeCounter
{
    public class LaserChargeCounterController
    {
        private readonly LaserChargeCounterView _view;

        public LaserChargeCounterController(LaserChargeCounterView view, CustomEcsWorld world)
        {
            _view = view;
            world.LaserChargeChange += UpdateCounter;
        }

        private void UpdateCounter(int value)
        {
            _view.Text.text = $"Laser charges: {value}";
        }
    }
}