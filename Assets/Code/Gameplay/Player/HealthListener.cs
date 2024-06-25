using UnityEngine;

namespace NewTankio.Code.Gameplay.Player
{
    public sealed class HealthListener : MonoBehaviour
    {
        public Health Health;

        private void OnEnable()
        {
            Health.TakenDamage += HealthOnTakenDamage;
        }
        
        private void OnDisable()
        {
            Health.TakenDamage -= HealthOnTakenDamage;
        }

        private void HealthOnTakenDamage(float obj)
        {
            Debug.LogWarning($"Current hp: {obj}");
        }
        
        
    }
}