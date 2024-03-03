using NewTankio.Code.Infrastructure.GameStates;
using VContainer;
namespace NewTankio.Code.Tools.StateMachine
{
    public sealed class StateFactory
    {
        private readonly IObjectResolver _resolver;
        
        public StateFactory(IObjectResolver resolver) => 
            _resolver = resolver;

        public TState GetState<TState>() where TState : class, IExitable => 
            _resolver.Resolve<TState>();
    }
}
