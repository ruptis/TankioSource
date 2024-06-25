using NewTankio.Code.Gameplay.Player.Mobility;
using UnityEngine;

namespace NewTankio.Code.Gameplay.Player
{
    public class Bullet : MonoBehaviour
    {
        public Movement Movement;
        public TriggerObserver TriggerObserver;

        private float _timer;
        private bool _stopped;

        private void OnEnable() => TriggerObserver.TriggerEntered += OnTriggerEntered;
        
        private void OnDisable() => TriggerObserver.TriggerEntered -= OnTriggerEntered;

        private void Update()
        {
            if (_stopped) return;
            
            if (Movement.CurrentVelocity == Vector2.zero)
            {
                Debug.Log(_timer);
                _stopped = true;
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        public void Fire(Vector2 velocity)
        {
            Movement.AddVelocity(velocity);
            _timer = 0.0f;
            _stopped = false;
        }

        private void OnTriggerEntered(ClonedObject thisObject, ClonedObject otherObject)
        {
            if (otherObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(new BulletDamage());
                Destroy(thisObject.gameObject);
                Debug.LogWarning("Bullet damaged");
            }
        }
        
        private class BulletDamage : IDamage
        {
            public float Damage => 20;
        }
    }
}
