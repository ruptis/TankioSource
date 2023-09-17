using NewTankio.Code.Infrastructure.GameStates;
using NewTankio.Code.Tools.StateMachine;
using VContainer.Unity;
namespace NewTankio.Code.Infrastructure
{
    public sealed class GameRunner : IStartable
    {
        private readonly StateMachine<IGameState> _stateMachine;
        public GameRunner(StateMachine<IGameState> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Start()
        {
            _stateMachine.Enter<BootstrapGameState>();
        }
    }
}
