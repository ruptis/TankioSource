using System;
using NewTankio.Code.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
namespace NewTankio.Code.Services.InputService
{
    public sealed class InputService : IInputService
    {
        private readonly Controls _controls = new();
        public Vector2 Movement { get; private set; }
        public Vector2 MousePosition { get; private set; }
        public bool Fire { get; private set; }
        public Action OnDebug { get; set; }

        public void Enable()
        {
            _controls.Player.Movement.performed += OnMovement;
            _controls.Player.Movement.canceled += OnMovement;

            _controls.Player.Mouse.performed += OnMouse;

            _controls.Player.Fire.performed += OnFire;
            _controls.Player.Fire.canceled += OnFire;

            _controls.Player.Debug.performed += OnDebugSwitch;

            _controls.Enable();
        }

        public void Disable()
        {
            _controls.Player.Movement.performed -= OnMovement;
            _controls.Player.Movement.canceled -= OnMovement;

            _controls.Player.Mouse.performed -= OnMouse;

            _controls.Player.Fire.performed -= OnFire;
            _controls.Player.Fire.canceled -= OnFire;

            _controls.Player.Debug.performed -= OnDebugSwitch;

            _controls.Disable();
        }

        private void OnMovement(InputAction.CallbackContext obj)
        {
            Movement = obj.ReadValue<Vector2>();
        }

        private void OnMouse(InputAction.CallbackContext obj)
        {
            MousePosition = obj.ReadValue<Vector2>();
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
            Fire = obj.ReadValueAsButton();
        }

        private void OnDebugSwitch(InputAction.CallbackContext obj)
        {
            OnDebug?.Invoke();
        }
    }
}
