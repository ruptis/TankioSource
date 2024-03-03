using NewTankio.Code.Tools.StateMachine;
namespace NewTankio.Code.Infrastructure.GameStates
{
    public sealed class GameStateMachine : StateMachine
    {
        public GameStateMachine(StateFactory stateFactory) : base(stateFactory)
        {}
    }

}
