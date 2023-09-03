using NewTankio.Tools.StateMachine;
namespace NewTankio.Infrastructure.GameStates
{
    public sealed class GameStateMachine : StateMachine<IGameState>
    {
        public GameStateMachine(IStateFactory<IGameState> stateFactory) : base(stateFactory)
        {}
    }

}
