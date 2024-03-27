using UnityEngine;
namespace NewTankio.Code.Gameplay.Player.Mobility
{
    public sealed class RandomMovement : MonoBehaviour
    {
        private Vector2 _direction;
        private float _cooldown;
        
        public Movement Movement;

        private void Update()
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0f)
            {
                _cooldown = Random.value * 6f;
                ChangeDirection();
            }
            
            Movement.Move(_direction);
        }

        private void ChangeDirection() => _direction = Quaternion.Euler(0f, 0f, Random.value * 360f) * Vector3.one; 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 transformPosition = transform.position;
            Gizmos.DrawLine(transformPosition, transformPosition + (Vector3)_direction);
        }
    }
}
