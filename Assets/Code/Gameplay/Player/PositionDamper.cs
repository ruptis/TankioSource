using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public struct PositionDamper
    {
        private Vector2 _velocity;
        private Vector2 _acceleration;

        public Vector2 Damp(Vector2 desiredVelocity, float halflife, float deltaTime)
        {
            var y = SpringUtils.HalflifeToDamping(halflife) / 2.0f;
            Vector2 j0 = _velocity - desiredVelocity;
            Vector2 j1 = _acceleration + j0 * y;
            var eydt = SpringUtils.FastNegExp(deltaTime * y);

            _velocity = eydt * (j0 + j1 * deltaTime) + desiredVelocity;
            _acceleration = eydt * (_acceleration - j1 * (y * deltaTime));
            
            return eydt * (-j1 / (y * y) + (-j0 - j1 * deltaTime) / y) +
                j1 / (y * y) + j0 / y + desiredVelocity * deltaTime;
        }

        public void AddVelocity(Vector2 velocity) => _velocity += velocity;
    }
}
