using NewTankio.Infrastructure.GameStates;
using NewTankio.Services.InputService;
using NewTankio.Tools.StateMachine;
using VContainer;
using VContainer.Unity;
namespace NewTankio.Infrastructure
{
    public sealed class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<BootstrapGameState>(Lifetime.Singleton);
            builder.Register<IStateFactory<IGameState>, GameStateFactory>(Lifetime.Singleton);
            builder.Register<StateMachine<IGameState>, GameStateMachine>(Lifetime.Singleton);
        }
    }
}
