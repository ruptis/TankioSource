using NewTankio.Code.Gameplay.Player.Mobility;
using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public class Bullet : MonoBehaviour
    {
        public Movement Movement;
        
        public void Fire(Vector2 velocity)
        {
            Movement.AddVelocity(velocity);
            _timer = 0.0f;
            _stopped = false;
        }

        private float _timer;
        private bool _stopped;

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
    }
}
