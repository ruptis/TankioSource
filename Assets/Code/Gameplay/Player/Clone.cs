using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public readonly struct Clone
    {
        private readonly SpriteRenderer _spriteRenderer;

        public Clone(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public void Activate(in Vector3 localPosition)
        {
            _spriteRenderer.transform.localPosition = localPosition;
            _spriteRenderer.enabled = true;
        }

        public void Deactivate()
        {
            _spriteRenderer.transform.localPosition = Vector3.zero;
            _spriteRenderer.enabled = false;
        }
    }
}
