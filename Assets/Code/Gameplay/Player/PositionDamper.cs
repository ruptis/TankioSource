using UnityEngine;
namespace NewTankio.Gameplay.Player
{
    public struct PositionDamper
    {
        private MovementData _movementData;

        private Vector2 Velocity
        { get => _movementData.Velocity;
          set => _movementData.Velocity = value; }
        private Vector2 Acceleration
        { get => _movementData.Acceleration;
          set => _movementData.Acceleration = value; }

        public Vector2 Damp(Vector2 desiredVelocity, float halflife, float deltaTime)
        {
            var y = SpringUtils.HalflifeToDamping(halflife) / 2.0f;
            Vector2 j0 = Velocity - desiredVelocity;
            Vector2 j1 = Acceleration + j0 * y;
            var eydt = SpringUtils.FastNegExp(deltaTime * y);

            Velocity = eydt * (j0 + j1 * deltaTime) + desiredVelocity;
            Acceleration = eydt * (Acceleration - j1 * (y * deltaTime));
            
            return eydt * (-j1 / (y * y) + (-j0 - j1 * deltaTime) / y) +
                j1 / (y * y) + j0 / y + desiredVelocity * deltaTime;
        }

        public void AddVelocity(Vector2 velocity) => Velocity += velocity;
    }
}
