using NewTankio.Code.Infrastructure.GameplayStates;
using VContainer.Unity;
namespace NewTankio.Code.Infrastructure
{
    public sealed class GameplayStartup : IStartable
    {
        private readonly GameplayStateMachine _stateMachine;
        public GameplayStartup(GameplayStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Start()
        {
            _stateMachine.Enter<GameplayInitializationState>();  
        }
    }
}
