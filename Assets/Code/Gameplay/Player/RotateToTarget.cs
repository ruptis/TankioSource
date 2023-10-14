using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class RotateToTarget : MonoBehaviour
    {
        private Vector2 _target;
        public Rotation Rotation;

        private void Update()
        {
            Vector2 direction = (_target - (Vector2)transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rotation.Rotate(angle);
        }

        public void Rotate(Vector2 target) => _target = target;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(transform.position, _target);
        }
    }
}
