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

        private void OnStay(Collider2D arg1, Collider2D arg2)
        {
            Debug.Log($"Stayed: {arg1.name} {arg2.name} in frame {Time.frameCount}");
        }
    }
}
