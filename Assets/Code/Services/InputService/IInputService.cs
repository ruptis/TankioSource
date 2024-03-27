using System;
using UnityEngine;
namespace NewTankio.Code.Services.InputService
{
    public interface IInputService
    {
        public Vector2 Movement { get; }
        public Vector2 MousePosition { get; }
        public bool Fire { get; }
        public Action OnDebug { get; set; }

        public void Enable();
        public void Disable();
    }
}
