using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Clone : MonoBehaviour
    {
        private Transform _parentTransform;
        private Quaternion _previousParentRotation;

        private void OnEnable()
        {
            Transform parent = transform.parent;
            _parentTransform = parent;
            _previousParentRotation = parent.rotation;
        }

        private void LateUpdate()
        {
            Quaternion parentRotation = _parentTransform.rotation;
            Quaternion rotationDifference = parentRotation * Quaternion.Inverse(_previousParentRotation);

            Transform currentTransform = transform;
            currentTransform.localPosition = Quaternion.Inverse(rotationDifference) * transform.localPosition;
            
            _previousParentRotation = parentRotation;
        }
        
        public void Activate(in Vector3 localPosition)
        {
            gameObject.SetActive(true);
            transform.localPosition = Quaternion.Inverse(_parentTransform.rotation) * localPosition;
        }
        public void Deactivate()
        {
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

}
