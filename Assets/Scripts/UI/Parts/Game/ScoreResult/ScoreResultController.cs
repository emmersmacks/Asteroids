using Data.Bases;

namespace UI.Parts.Game.ScoreResult
{
    public class ScoreResultController : Controller<ScoreResultView>
    {
        public ScoreResultController(ScoreResultView view) : base(view)
        {
        }

        public void SetScore(int score)
        {
            _view.ScoreField.text = score.ToString();
        }
    }
}