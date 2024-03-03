using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services.CoordinateWrapper
{
    public class RectangleWrapper : ICoordinateWrapper
    {
        private readonly IMapBoundaries _boundaries;

        public RectangleWrapper(IMapBoundaries boundaries) => 
            _boundaries = boundaries;

        public Vector2 WrapPoint(in Vector2 point) 
            => new(
                WrapCoordinate(point.x, _boundaries.MinPoint.x, _boundaries.MaxPoint.x), 
                WrapCoordinate(point.y, _boundaries.MinPoint.y, _boundaries.MaxPoint.y));
        
        private static float WrapCoordinate(float value, float minValue, float maxValue) 
            => Mathf.Repeat(value - minValue, maxValue - minValue) + minValue;
    }
}
