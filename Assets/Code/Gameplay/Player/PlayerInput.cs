using NewTankio.Services.InputService;
using UnityEngine;
using VContainer;
namespace NewTankio.Gameplay.Player
{
    public class PlayerInput : MonoBehaviour
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
