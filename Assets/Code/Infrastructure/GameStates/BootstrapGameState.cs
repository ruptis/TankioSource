using NewTankio.Code.Services;
using NewTankio.Code.Services.InputService;
using NewTankio.Code.Tools.StateMachine;
namespace NewTankio.Code.Infrastructure.GameStates
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
