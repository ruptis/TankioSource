using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Knockback : MonoBehaviour
    {
        public Movement Movement;
        public TriggerObserver TriggerObserver;
        public float BounceFactor = 1f;

        private void OnEnable()
        {
            TriggerObserver.TriggerStayed += OnTriggerStayed;
        }

        private void OnDisable()
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
