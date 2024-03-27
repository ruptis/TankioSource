using UnityEngine;
namespace NewTankio.Code.Gameplay.Player.Mobility
{
    public class Rotation : MonoBehaviour
    {
        private float _rotationAngle;
        private RotationDamper _rotationDamper;

        public float HalfLife = 0.3f;

        public Vector2 CurrentVelocity => transform.up * (_rotationDamper.AngularVelocity / Mathf.Rad2Deg);

        private void Update()
        {
            var angle = _rotationDamper.Damp(transform.eulerAngles.z, _rotationAngle, HalfLife, Time.deltaTime);
            transform.eulerAngles = new Vector3(0f, 0f, angle);
            _rotationAngle = 0f;
        }

        public void Rotate(float angle) => _rotationAngle += angle;
    }
}
