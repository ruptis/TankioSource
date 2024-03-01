using NewTankio.Code.Infrastructure.GameplayStates;
using UnityEngine;
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
            Debug.Log("GameplayStartup");
            _stateMachine.Enter<GameplayInitializationState>();    
        }
    }
}
