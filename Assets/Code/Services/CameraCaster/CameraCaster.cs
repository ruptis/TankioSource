using UnityEngine;
namespace NewTankio.Code.Services.CameraCaster
{
    public class CameraCaster : ICameraCaster
    {
        private readonly Camera _camera;
        public CameraCaster(Camera camera)
        {
            _camera = camera;
        }
        
        public Vector2 CastToWorld(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }
    }
}
