using VContainer;
namespace NewTankio.Code.Tools.StateMachine
{
    public class StateFactory
    {
        private readonly IObjectResolver _resolver;
        
        public StateFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public TState GetState<TState>() where TState : class, IExitable
        {
            return _resolver.Resolve<TState>();
        }
    }
}
