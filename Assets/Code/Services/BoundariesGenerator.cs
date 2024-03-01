using UnityEngine;
namespace NewTankio.Code.Services
{
    public abstract class BoundariesGenerator : MonoBehaviour
    {
        public abstract BoundariesStaticData GenerateBoundariesData();
    }
}
