using UnityEngine;
namespace NewTankio.Code.Services.CoordinateWrapper
{
    public interface ICoordinateWrapper
    {
        public Vector2 WrapPoint(in Vector2 point);
    }
}
