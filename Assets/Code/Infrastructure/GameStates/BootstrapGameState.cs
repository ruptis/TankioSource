using NewTankio.Code.Services.InputService;
using NewTankio.Code.Tools.StateMachine;
using UnityEngine.SceneManagement;
namespace NewTankio.Code.Infrastructure.GameStates
{
    public class BootstrapGameState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;

        public BootstrapGameState(GameStateMachine gameStateMachine, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
        }
        public void Exit()
        {}
        public void Enter()
        {
            _inputService.Enable();

            SceneManager.LoadScene("Main");
            _gameStateMachine.Enter<MainGameState>();
        }
    }
}
