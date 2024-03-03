using NewTankio.Code.Tools.StateMachine;
namespace NewTankio.Code.Infrastructure.GameplayStates
{
    public sealed class GameplayStateMachine : StateMachine
    {
        public GameplayStateMachine(StateFactory stateFactory) : base(stateFactory)
        {}
    }
}
