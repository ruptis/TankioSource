using NewTankio.Code.Tools.StateMachine;
namespace NewTankio.Code.Infrastructure.GameStates
{
    public sealed class GameStateMachine : StateMachine<IGameState>
    {
        public GameStateMachine(IStateFactory<IGameState> stateFactory) : base(stateFactory)
        {}
    }

}
