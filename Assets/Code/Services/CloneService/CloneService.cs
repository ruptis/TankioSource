using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services.CloneService
{
    public sealed class CloneService : ICloneService
    {
        private readonly Vector2 _size;

        public CloneService(IMapBoundaries boundaries) => 
            _size = boundaries.MaxPoint - boundaries.MinPoint;

        public Vector2 GetClonePosition(in Vector2 direction) => 
            -direction * _size;

        public Vector2 GetCornerClonePosition(in Vector2 firstDirection, in Vector2 secondDirection) => 
            -(firstDirection + secondDirection) * _size;
    }
}
