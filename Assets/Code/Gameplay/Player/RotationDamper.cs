using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public struct RotationDamper
    {
        private float _angularVelocity;
        
        public float Damp(float currentAngle, float desiredAngle, float halflife, float deltaTime)
        {
            var y = SpringUtils.HalflifeToDamping(halflife) / 2.0f;
            var j0 = Mathf.DeltaAngle(desiredAngle, currentAngle);
            var j1 = _angularVelocity + j0 * y;
            var eydt = SpringUtils.FastNegExp(y * deltaTime);

            _angularVelocity = eydt * (_angularVelocity - j1 * (y * deltaTime));
            return eydt * (j0 + j1 * deltaTime) + desiredAngle;
        }
    }
}
