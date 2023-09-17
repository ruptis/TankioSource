using UnityEngine;
namespace NewTankio.Code.Services.InputService
{
    public interface IInputService
    {
        public Vector2 Movement { get; }
        public Vector2 MousePosition { get; }

        public void Enable();
        public void Disable();
    }
}
