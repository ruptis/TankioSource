using NewTankio.Code.Infrastructure.GameStates;
using UnityEngine;
using VContainer.Unity;
namespace NewTankio.Code.Infrastructure
{
    public sealed class GameStartup : IStartable
    {
        private readonly GameStateMachine _stateMachine;
        public GameStartup(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Start()
        {
            Debug.Log("GameStartup");
            _stateMachine.Enter<BootstrapGameState>();
        }
    }
}
