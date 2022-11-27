using Infrastructure;
using Leopotam.Ecs;

namespace UI.Parts.Game.LaserСhargeCounter
{
    public class LaserChargeCounterController : Controller<LaserChargeCounterView>
    {
        public LaserChargeCounterController(LaserChargeCounterView view, CustomEcsWorld world) : base(view)
        {
            world.LaserChargeChange += UpdateCounter;
        }

        private void UpdateCounter(int value)
        {
            _view.Text.text = $"Laser charges: {value}";
        }
    }
}