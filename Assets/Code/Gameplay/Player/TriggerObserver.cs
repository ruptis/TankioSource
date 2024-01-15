using System;
using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class TriggerObserver : MonoBehaviour
    {
        private Collider2D _collider;
        
        public Action<Collider2D, Collider2D> TriggerEntered;
        public Action<Collider2D, Collider2D> TriggerStayed;
        public Action<Collider2D, Collider2D> TriggerExited;

        private void Awake() => _collider = GetComponent<Collider2D>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered?.Invoke(_collider, other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStayed?.Invoke(_collider, other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExited?.Invoke(_collider, other);
        }
    }
}
