using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class DebugTriggerListener : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;

        private void OnEnable()
        {
            TriggerObserver.TriggerStayed += OnStay;
        }

        private void OnDisable()
        {
            TriggerObserver.TriggerStayed -= OnStay;
        }

        private void OnStay(ClonedObject arg1, ClonedObject arg2)
        {
            Debug.Log($"Stayed: {arg1.name} {arg2.name} in frame {Time.frameCount}");
        }
    }
}
