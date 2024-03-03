using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services.CloneService
{
    public class CloneService : ICloneService
    {
        private readonly IMapBoundaries _mapBoundariesService;
        
        public CloneService(IMapBoundaries mapBoundariesService) => 
            _mapBoundariesService = mapBoundariesService;

        public Vector2 GetClonePosition(in Vector2 originalPosition, in Boundary boundary)
        {
            Vector2 closestPoint = boundary.ClosestPoint(originalPosition);
            Vector2 oppositeClosestPoint = GetOppositeClosestPoint(boundary, originalPosition);
            Vector2 direction = GetDirection(oppositeClosestPoint, closestPoint);
            var distance = Vector2.Distance(oppositeClosestPoint, closestPoint);
            return GetClonePosition(direction, distance);
        }
        
        public Vector2 GetCornerClonePosition(in Boundary firstBoundary, in Boundary secondBoundary)
        {
            Vector2 cornerPoint = _mapBoundariesService.GetIntersectionPoint(firstBoundary, secondBoundary);
            Vector2 oppositeCornerPoint = GetOppositeCornerPoint(firstBoundary, secondBoundary);
            Vector2 cornerDirection = GetDirection(oppositeCornerPoint, cornerPoint);
            var distanceToCorner = Vector2.Distance(oppositeCornerPoint, cornerPoint);
            return GetClonePosition(cornerDirection, distanceToCorner);
        }

        private Vector2 GetOppositeClosestPoint(Boundary boundary, Vector2 position)
        {
            Boundary oppositeBoundary = _mapBoundariesService.GetOppositeBoundary(boundary);
            return oppositeBoundary.ClosestPoint(position);
        }
        
        private Vector2 GetOppositeCornerPoint(Boundary firstBoundary, Boundary secondBoundary)
        {
            Boundary firstOppositeBoundary = _mapBoundariesService.GetOppositeBoundary(firstBoundary);
            Boundary secondOppositeBoundary = _mapBoundariesService.GetOppositeBoundary(secondBoundary);
            return _mapBoundariesService.GetIntersectionPoint(firstOppositeBoundary, secondOppositeBoundary);
        }
        
        private static Vector2 GetClonePosition(Vector2 direction, float distance) 
            => direction * distance;
        
        private static Vector2 GetDirection(Vector2 firstPoint, Vector2 secondPoint) 
            => (firstPoint - secondPoint).normalized;
    }
}
