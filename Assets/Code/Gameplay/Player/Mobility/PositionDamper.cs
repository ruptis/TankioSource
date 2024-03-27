using UnityEngine;
namespace NewTankio.Code.Gameplay.Player.Mobility
{
    public struct PositionDamper
    {
        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; private set; }

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
