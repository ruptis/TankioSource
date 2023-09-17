using NewTankio.Code.Services.InputService;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class PlayerInput : MonoBehaviour
    {
        private IInputService _inputService;
        public Movement Movement;

        [Inject]
        public void Construst(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Update()
        {
            Movement.Move(_inputService.Movement);
        }
    }
}
