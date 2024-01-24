using NewTankio.Code.Services.CoordinateWrapper;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Tools
{
    public class PointTest : MonoBehaviour
    {
        private IMapBoundaries _mapBoundaries;
        private ICoordinateWrapper _coordinateWrapper;

        public float Radius = 1f;
        public float LineLength = 100f;

        [Inject]
        public void Construct(IMapBoundaries mapBoundaries, ICoordinateWrapper coordinateWrapper)
        {
            _mapBoundaries = mapBoundaries;
            _coordinateWrapper = coordinateWrapper;
        }
        private void OnDrawGizmos()
        {
            if (_mapBoundaries == null)
                return;

            Vector2 currentPosition = transform.position;
            Vector2 wrappedPosition = _coordinateWrapper.WrapPoint(currentPosition);

            if (_mapBoundaries.TryGetCrossedBoundary(currentPosition, out Boundary boundary))
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(boundary.ClosestPoint(currentPosition), Radius);
            
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(currentPosition, Radius);
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(wrappedPosition, Radius);
            
                Gizmos.color = Color.blue;
                Vector2 normal = boundary.Normal;
                var perpendicular = new Vector2(-normal.y, normal.x);

                Gizmos.DrawRay(boundary.Position, perpendicular * LineLength);
                Gizmos.DrawRay(boundary.Position, -perpendicular * LineLength);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(currentPosition, Radius);
            }
        }
    }
}
