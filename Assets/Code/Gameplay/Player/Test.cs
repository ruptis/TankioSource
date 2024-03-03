using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class Test : MonoBehaviour
    {
        public BoundaryObserver BoundaryObserver;
        
        private void Start()
        {
            BoundaryObserver.BoundaryEntered += _ => Debug.Log("Entered");
            BoundaryObserver.BoundaryExited += boundary => Debug.Log("Exited " + boundary);
        }
    }
}
