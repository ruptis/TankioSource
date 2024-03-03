using NewTankio.Code.Infrastructure.GameStates;
using NewTankio.Code.Services.InputService;
using NewTankio.Code.Tools.StateMachine;
using VContainer;
using VContainer.Unity;
namespace NewTankio.Code.Infrastructure
{
    public sealed class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            IsRoot = true;
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<BootstrapGameState>(Lifetime.Singleton);
            builder.Register<MainGameState>(Lifetime.Singleton);
            builder.Register<StateFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameStartup>();
        }
    }
}
