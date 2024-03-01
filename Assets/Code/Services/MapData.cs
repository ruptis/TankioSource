using UnityEngine;
namespace NewTankio.Code.Services
{
    [CreateAssetMenu(fileName = "MapData", menuName = "NewTankio/MapData", order = 0)]
    public sealed class MapData : ScriptableObject
    {
        public BoundariesStaticData BoundariesStaticData;
    }
}
