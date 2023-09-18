using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public readonly struct Clone
    {
        private readonly Transform _transform;

        public Clone(Transform transform)
        {
            _transform = transform;
        }

        public void Activate(in Vector3 localPosition)
        {
            _transform.transform.localPosition = localPosition;
            _transform.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _transform.transform.localPosition = Vector3.zero;
            _transform.gameObject.SetActive(false);
        }
    }
}
