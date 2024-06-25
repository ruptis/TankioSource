using System;
using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class TriggerObserver : MonoBehaviour
    {
        public ClonedObject Parent { get; private set; }

        private void Awake()
        {
            Parent = GetComponentInParent<ClonedObject>();
        }
        
        public Action<ClonedObject, ClonedObject> TriggerEntered;
        public Action<ClonedObject, ClonedObject> TriggerStayed;
        public Action<ClonedObject, ClonedObject> TriggerExited;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out TriggerObserver otherObserver))
                TriggerEntered?.Invoke(Parent, otherObserver.Parent);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out TriggerObserver otherObserver))
                TriggerStayed?.Invoke(Parent, otherObserver.Parent);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out TriggerObserver otherObserver))
                TriggerExited?.Invoke(Parent, otherObserver.Parent);
        }
    }
}
