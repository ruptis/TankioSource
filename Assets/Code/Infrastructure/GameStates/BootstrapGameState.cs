using NewTankio.Services.InputService;
using NewTankio.Tools.StateMachine;
namespace NewTankio.Infrastructure.GameStates
{
    public class BootstrapGameState : IGameState, IState
    {
        private readonly IInputService _inputService;
        
        public BootstrapGameState(IInputService inputService)
        {
            _inputService = inputService;
        }
        public void Exit()
        {}
        public void Enter()
        {
            _inputService.Enable();
        }
    }
}
