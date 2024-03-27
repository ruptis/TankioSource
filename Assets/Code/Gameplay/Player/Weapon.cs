using NewTankio.Code.Gameplay.Player.Mobility;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace NewTankio.Code.Gameplay.Player
{
    public class Weapon : MonoBehaviour
    {
        public Bullet BulletPrefab;
        public Transform FirePoint;
        public Movement CharacterMovement;
        public Rotation CharacterRotation;
        public float FireRate;
        public float BulletSpeed;
        public float BulletHalfSpeed;
        
        public bool ConsiderCharacterVelocity;
        
        private float _fireTimer;
        
        private IObjectResolver _resolver;
        
        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        private void Update()
        {
            _fireTimer += Time.deltaTime;
        }

        public void Fire()
        {
            if (_fireTimer >= FireRate)
            {
                _fireTimer = 0f;
                Bullet bullet = _resolver.Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
                bullet.GetComponent<Movement>().Halflife = BulletHalfSpeed;
                Vector2 bulletVelocity = BulletVelocity;
                bullet.Fire(bulletVelocity);
            }
        }
        private Vector2 BulletVelocity
        {
            get
            {
                Vector2 bulletVelocity = (Vector2)FirePoint.up * BulletSpeed;
                if (ConsiderCharacterVelocity)
                {
                    bulletVelocity += CharacterMovement.CurrentVelocity;
                    bulletVelocity += CharacterRotation.CurrentVelocity;
                }
                return bulletVelocity;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(FirePoint.position, FirePoint.position + (Vector3)BulletVelocity);
        }
    }
}
