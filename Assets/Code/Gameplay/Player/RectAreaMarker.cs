using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class RectAreaMarker : MonoBehaviour
    {
        private Rect _rect;

        public Vector2 Offset = Vector2.zero;
        public Vector2 Size = Vector2.one;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 position = (Vector3)Offset + transform.position;
            Gizmos.DrawWireCube(position, ScaledSize);
        }

        private Vector2 ScaledSize => Vector2.Scale(Size, transform.lossyScale);
        public Rect Rect
        {
            get
            {
                _rect.center = Offset + (Vector2)transform.position;
                _rect.size = ScaledSize;
                return _rect;
            }
        }
    }
}
