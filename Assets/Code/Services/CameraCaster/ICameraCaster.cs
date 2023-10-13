using UnityEngine;
namespace NewTankio.Code.Services.CameraCaster
{
    public interface ICameraCaster
    {
        Vector2 CastToWorld(Vector2 mousePosition);
    }
}
