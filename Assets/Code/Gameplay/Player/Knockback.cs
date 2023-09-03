using UnityEngine;
namespace NewTankio.Gameplay.Player
{
    public class Knockback : MonoBehaviour
    {
        public Movement Movement;
        public TriggerObserver TriggerObserver;
        public float BounceFactor = 1f;

        private void Awake()
        {
            TriggerObserver.TriggerStayed += OnTriggerStayed;
        }

        private void OnDestroy()
        {
            TriggerObserver.TriggerStayed -= OnTriggerStayed;
        }

        private void OnTriggerStayed(Collider2D otherCollider)
        {
            Vector3 direction = (transform.position - otherCollider.transform.position).normalized;
            Vector3 velocity = direction * BounceFactor;
            Movement.AddVelocity(velocity);
        }
    }
}
