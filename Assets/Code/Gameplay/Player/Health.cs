using System;
using UnityEngine;

namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Health : MonoBehaviour
    {
        [SerializeField] private float _max;
        private float _current;

        private void OnValidate()
        {
            _current = _max;
        }

        public event Action<float> TakenDamage;

        public event Action Death;

        public void Decrease(float damage)
        {
            _current -= damage;
            TakenDamage?.Invoke(_current);
            
            if (_current < 0) 
                Death?.Invoke();
        }
    }
}