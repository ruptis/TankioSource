using System;
using UnityEngine;
namespace NewTankio.Gameplay.Player
{
    public class TriggerObserver : MonoBehaviour
    {
        public Action<Collider2D> TriggerEntered;
        public Action<Collider2D> TriggerStayed;
        public Action<Collider2D> TriggerExited;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered?.Invoke(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStayed?.Invoke(other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExited?.Invoke(other);
        }
    }
}
