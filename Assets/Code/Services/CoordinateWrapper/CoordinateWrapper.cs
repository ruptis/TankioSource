using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services.CoordinateWrapper
{
    public class CoordinateWrapper : ICoordinateWrapper
    {
        private readonly IMapBoundaries _mapBoundaries;

        public CoordinateWrapper(IMapBoundaries mapBoundaries)
        {
            _mapBoundaries = mapBoundaries;
        }
        
        public Vector2 WrapPoint(in Vector2 point)
        {
            if (!_mapBoundaries.TryGetCrossedBoundary(point, out Boundary boundary))
                return point;
            
            Vector2 closestPoint = boundary.ClosestPoint(point);
            Vector2 direction = point - closestPoint; 
            
            Boundary oppositeBoundary = _mapBoundaries.GetOppositeBoundary(boundary);
            Vector2 oppositeClosestPoint = oppositeBoundary.ClosestPoint(point);
            
            Vector2 wrappedPoint = oppositeClosestPoint + direction;
            
            return _mapBoundaries.IsInside(wrappedPoint) ? wrappedPoint : WrapPoint(wrappedPoint);
        }
    }
}
