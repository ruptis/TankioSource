using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Clone : MonoBehaviour
    {
        private Transform _parentTransform;
        private Quaternion _previousParentRotation;
        private float _previousParentScale;

        private void OnEnable()
        {
            Transform parent = transform.parent;
            _parentTransform = parent;
            _previousParentRotation = parent.rotation;
            _previousParentScale = parent.localScale.x;
        }

        private void LateUpdate()
        {
            Quaternion parentRotation = _parentTransform.rotation;
            Quaternion rotationDifference = parentRotation * Quaternion.Inverse(_previousParentRotation);
            var parentScale = _parentTransform.localScale.x;
            var scaleDifference = _previousParentScale / parentScale;

            Transform currentTransform = transform;
            currentTransform.localPosition = Quaternion.Inverse(rotationDifference) * transform.localPosition * scaleDifference;

            _previousParentRotation = parentRotation;
            _previousParentScale = parentScale;
        }

        public void Activate(in Vector3 localPosition)
        {
            gameObject.SetActive(true);
            transform.localPosition = Quaternion.Inverse(_parentTransform.rotation) * localPosition * (1f / _parentTransform.localScale.x);
        }
        public void Deactivate()
        {
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

}
