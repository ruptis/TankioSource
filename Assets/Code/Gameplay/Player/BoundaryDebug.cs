using NewTankio.Code.Services;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public class BoundaryDebug : MonoBehaviour
    {
        private IMapBoundaries _mapBoundaries;
        private bool _isInitialized;
        
        [Inject]
        public void Construct(IMapBoundaries mapBoundariesService)
        {
            _mapBoundaries = mapBoundariesService;
            _isInitialized = true;
        }

        private void OnDrawGizmos()
        {
            if (!_isInitialized) return;
            foreach (Boundary boundary in _mapBoundaries.Boundaries)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(boundary.Position, 1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(boundary.Position, boundary.Position + boundary.Normal * 6f);
            }
        }
    }
}
