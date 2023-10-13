using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class Rotation : MonoBehaviour
    {
        private float _rotationAngle;
        private Vector2 _desiredDirection;
        private RotationDamper _rotationDamper;

        public float HalfLife = 0.3f;

        private void Update()
        {
            var angle = _rotationDamper.Damp(transform.eulerAngles.z, _rotationAngle, HalfLife, Time.deltaTime);
            transform.eulerAngles = new Vector3(0f, 0f, angle);
            _rotationAngle = 0f;
        }

        public void Rotate(float angle) => _rotationAngle += angle;
    }
}
