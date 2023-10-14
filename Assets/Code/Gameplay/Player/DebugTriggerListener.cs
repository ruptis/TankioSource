using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class DebugTriggerListener : MonoBehaviour
    {
        public TriggerEmitter TriggerEmitter;

        private void OnEnable()
        {
            TriggerEmitter.TriggerStayed += OnStay;
        }
        
        private void OnDisable()
        {
            TriggerEmitter.TriggerStayed -= OnStay;
        }
        
        private void OnStay(Collider2D arg1, Collider2D arg2)
        {
            Debug.Log(arg1.gameObject.name + ", Frame: " + Time.frameCount);
        }
    }
}
