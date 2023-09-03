using NewTankio.Infrastructure.GameStates;
using NewTankio.Tools.StateMachine;
using VContainer;
using VContainer.Unity;
namespace NewTankio.Infrastructure
{
    public sealed class GameRunner : LifetimeScope
    {
        private void Start()
        {
            var stateMachine = Container.Resolve<StateMachine<IGameState>>();
            stateMachine.Enter<BootstrapGameState>();
        }
    }
}
