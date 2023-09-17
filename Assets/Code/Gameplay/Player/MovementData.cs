using System;
using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    [Serializable]
    public struct MovementData
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;
    }
}
