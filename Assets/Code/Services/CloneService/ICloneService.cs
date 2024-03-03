using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services.CloneService
{
    public interface ICloneService
    {
        public Vector2 GetClonePosition(in Vector2 originalPosition, in Boundary boundary);
        public Vector2 GetCornerClonePosition(in Boundary firstBoundary, in Boundary secondBoundary);
    }

}
