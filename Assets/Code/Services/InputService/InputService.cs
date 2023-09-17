using UnityEngine;
using UnityEngine.InputSystem;
namespace NewTankio.Code.Services.InputService
{
    public sealed class InputService : IInputService
    {
        private readonly Controls _controls = new();
        public Vector2 Movement { get; private set; }
        public Vector2 MousePosition { get; }

        public void Enable()
        {
            _controls.Player.Movement.performed += OnMovement;
            _controls.Player.Movement.canceled += OnMovement;

            _controls.Enable();
        }

        public void Disable()
        {
            _controls.Player.Movement.performed -= OnMovement;
            _controls.Player.Movement.canceled -= OnMovement;

            _controls.Disable();
        }

        private void OnMovement(InputAction.CallbackContext obj)
        {
            Movement = obj.ReadValue<Vector2>();
        }
    }
}
