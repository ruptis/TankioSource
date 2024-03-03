using UnityEngine;
namespace NewTankio.Code.Services.CloneService
{
    public interface ICloneService
    {
        public Vector2 GetClonePosition(in Vector2 direction);
        public Vector2 GetCornerClonePosition(in Vector2 firstDirection, in Vector2 secondDirection);
    }

}
