using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Knockback : MonoBehaviour
    {
        public Mobility.Movement Movement;
        public TriggerObserver TriggerObserver;
        public float BounceFactor = 1f;

        private void OnEnable() 
            => TriggerObserver.TriggerStayed += OnTriggerStayed;

        private void OnDisable() 
            => TriggerObserver.TriggerStayed -= OnTriggerStayed;

        private void OnTriggerStayed(Collider2D thisCollider, Collider2D otherCollider)
        {
            Vector3 direction = (thisCollider.transform.position - otherCollider.transform.position).normalized;
            Vector3 velocity = direction * BounceFactor;
            _debugVelocity = velocity;
            _debugTransformPosition = thisCollider.transform.position;
            _debugOtherColliderPosition = otherCollider.transform.position;
            Movement.AddVelocity(velocity);
        }

        private Vector3 _debugVelocity;
        private Vector3 _debugTransformPosition;
        private Vector3 _debugOtherColliderPosition;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _debugVelocity);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_debugTransformPosition, _debugOtherColliderPosition);
        }
    }
}
