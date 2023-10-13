using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class Movement : MonoBehaviour
    {
        private Vector2 _desiredMovement;
        private PositionDamper _damper;

        public float Halflife = 0.5f;
        public float Speed = 1.0f;

        private void Update()
        {
            Vector2 movement = _damper.Damp(_desiredMovement, Halflife, Time.deltaTime);
            transform.position += (Vector3)movement;
            _desiredMovement = Vector2.zero;
        }
        
        public void Move(Vector2 velocity) => _desiredMovement += velocity * Speed;

        public void AddVelocity(Vector2 velocity) => _damper.AddVelocity(velocity);
    }
}
