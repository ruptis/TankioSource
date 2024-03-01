using UnityEngine;
namespace NewTankio.Code.Services
{
    public class MapTransformProvider : ITransformProvider
    {
        public MapTransformProvider(Transform transform) => Transform = transform;
        public Transform Transform { get; }
    }
}
