using Data.Bases;

namespace UI.Parts.Game.ScoreResult
{
    public class ScoreResultController
    {
        private readonly ScoreResultView _view;

        public ScoreResultController(ScoreResultView view)
        {
            _view = view;
        }

        public void SetScore(int score)
        {
            _view.ScoreField.text = score.ToString();
        }
    }
}