﻿using UnityEngine;
namespace NewTankio.Code.Gameplay.Player.Mobility
{
    public struct RotationDamper
    {
        public float AngularVelocity { get; private set; }

        public float Damp(float currentAngle, float desiredAngle, float halflife, float deltaTime)
        {
            var y = SpringUtils.HalflifeToDamping(halflife) / 2.0f;
            var j0 = Mathf.DeltaAngle(desiredAngle, currentAngle);
            var j1 = AngularVelocity + j0 * y;
            var eydt = SpringUtils.FastNegExp(y * deltaTime);

            AngularVelocity = eydt * (AngularVelocity - j1 * (y * deltaTime));
            return eydt * (j0 + j1 * deltaTime) + desiredAngle;
        }
    }
}
