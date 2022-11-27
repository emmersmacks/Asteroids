namespace UI
{
    public abstract class Controller<T> where T : IView
    {
        protected T _view;
        
        public Controller(T view)
        {
            _view = view;
        }
    }
}