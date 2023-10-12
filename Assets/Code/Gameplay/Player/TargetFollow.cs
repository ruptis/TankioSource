using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class TargetFollow : MonoBehaviour
    {
        private Transform _transform;
        public Transform Target;

        private void LateUpdate()
        {
            _transform = transform;
            Vector3 currentPosition = _transform.position;
            Vector3 targetPosition = Target.position;
            var currentZ = currentPosition.z;
            currentPosition = targetPosition;
            currentPosition.z = currentZ;
            _transform.position = currentPosition;
        }
    }
}
