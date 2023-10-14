using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class TriggerRelay : TriggerEmitter
    {
        [SerializeField] private List<TriggerEmitter> _triggerEmitters;

        private void OnEnable()
        {
            foreach (TriggerEmitter emitter in _triggerEmitters)
            {
                emitter.TriggerEntered += OnTriggerEntered;
                emitter.TriggerExited += OnTriggerExited;
                emitter.TriggerStayed += OnTriggerStayed;
            }
        }

        private void OnDisable()
        {
            foreach (TriggerEmitter emitter in _triggerEmitters)
            {
                emitter.TriggerEntered -= OnTriggerEntered;
                emitter.TriggerExited -= OnTriggerExited;
                emitter.TriggerStayed -= OnTriggerStayed;
            }
        }

        private void OnTriggerStayed(Collider2D thisCollider, Collider2D otherCollider)
            => TriggerStayed?.Invoke(thisCollider, otherCollider);
        private void OnTriggerExited(Collider2D thisCollider, Collider2D otherCollider)
            => TriggerExited?.Invoke(thisCollider, otherCollider);
        private void OnTriggerEntered(Collider2D thisCollider, Collider2D otherCollider)
            => TriggerEntered?.Invoke(thisCollider, otherCollider);
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _triggerEmitters = new List<TriggerEmitter>(GetComponentsInChildren<TriggerEmitter>());
            _triggerEmitters.Remove(this);
        }
#endif
    }
}
