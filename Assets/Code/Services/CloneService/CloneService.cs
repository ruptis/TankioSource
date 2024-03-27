using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services.CloneService
{
    public sealed class CloneService : ICloneService
    {
        private readonly IMapBoundaries _boundaries;

        public CloneService(IMapBoundaries boundaries) => 
            _boundaries = boundaries;

        public Vector2 GetClonePosition(in Vector2 direction) =>
            -direction * _boundaries.Size;

        public Vector2 GetCornerClonePosition(in Vector2 firstDirection, in Vector2 secondDirection) => 
            -(firstDirection + secondDirection) * _boundaries.Size;
    }
}
