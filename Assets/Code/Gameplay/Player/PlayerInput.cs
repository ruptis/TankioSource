using NewTankio.Code.Gameplay.Player.Mobility;
using NewTankio.Code.Services.CameraCaster;
using NewTankio.Code.Services.InputService;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class PlayerInput : MonoBehaviour
    {
        private IInputService _inputService;
        private ICameraCaster _cameraCaster;
        public Movement Movement;
        public RotateToTarget Rotation;
        public Weapon Weapon;

        [Inject]
        public void Construst(IInputService inputService, ICameraCaster cameraCaster)
        {
            _inputService = inputService;
            _cameraCaster = cameraCaster;
            
            _inputService.OnDebug = () => Weapon.ConsiderCharacterVelocity = !Weapon.ConsiderCharacterVelocity;
        }
        
        private void Update()
        {
            Movement.Move(_inputService.Movement);
            Rotation.Rotate(_cameraCaster.CastToWorld(_inputService.MousePosition));
            if (_inputService.Fire)
            {
                Weapon.Fire();
            }
        }
    }
}
