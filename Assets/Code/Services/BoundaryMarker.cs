using UnityEngine;
namespace NewTankio.Code.Services
{
    public sealed class BoundaryMarker : MonoBehaviour
    {
        public Vector2 Normal => transform.up;
        public Vector2 Position => transform.position;
        
        public float VisibleLength = 1f;
    }
}
