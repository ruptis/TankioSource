using UnityEngine;

namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        public Health Health;

        public void TakeDamage(IDamage damage) => Health.Decrease(damage.Damage);
    }
}