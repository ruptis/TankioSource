using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class CameraFollow : MonoBehaviour
    {
        public Camera Camera;

        private void LateUpdate()
        {
            Transform cameraTransform = Camera.transform;
            var cameraZ = cameraTransform.position.z;
            cameraTransform.position = transform.position + new Vector3(0, 0, cameraZ);
        }
    }
}
