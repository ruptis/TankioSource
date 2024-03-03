using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Services.CloneService;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class ClonesController : MonoBehaviour
    {
        private const int MaxClones = 3;

        [SerializeField] private BoundaryObserver _boundaryObserver;
        [SerializeField] private Clone[] _clones = new Clone[MaxClones];

        private readonly Queue<Clone> _clonesQueue = new(MaxClones);
        private Dictionary<Vector2, Clone> _boundaryWithClone;
        private Clone _cornerClone;
        private HashSet<Vector2> _intersectedBoundaries;
        private ICloneService _cloneService;

        [Inject]
        public void Construct(IMapBoundaries mapBoundariesService, ICloneService cloneService)
        {
            _cloneService = cloneService;
            _intersectedBoundaries = new HashSet<Vector2>(mapBoundariesService.BoundariesCount);
            _boundaryWithClone = new Dictionary<Vector2, Clone>(mapBoundariesService.BoundariesCount);
        }

        private void Start()
        {
            foreach (Clone clone in _clones)
            {
                _clonesQueue.Enqueue(clone);
                clone.Deactivate();
            }
        }

        private void OnEnable()
        {
            _boundaryObserver.BoundaryEntered += OnBoundaryEntered;
            _boundaryObserver.BoundaryExited += OnBoundaryExited;
        }

        private void OnDisable()
        {
            _boundaryObserver.BoundaryEntered -= OnBoundaryEntered;
            _boundaryObserver.BoundaryExited -= OnBoundaryExited;
        }

        private void OnBoundaryEntered(Vector2 direction)
        {
            _intersectedBoundaries.Remove(direction);

            DeactivateBoundaryClone(direction);

            if (!IsSecondBoundaryEntered())
                DeactivateCornerClone();
        }

        private void OnBoundaryExited(Vector2 direction)
        {
            _intersectedBoundaries.Add(direction);

            ActivateBoundaryClone(in direction, GetBoundaryClone(in direction));

            if (!IsSecondBoundaryExited())
                ActivateCornerClone(_intersectedBoundaries.First(), _intersectedBoundaries.Last(), GetCornerClone());
        }

        private void DeactivateBoundaryClone(Vector2 direction)
        {
            if (_boundaryWithClone.TryGetValue(direction, out Clone clone))
                clone.Deactivate();
            _clonesQueue.Enqueue(clone);
        }
        
        private bool IsSecondBoundaryEntered() => 
            _intersectedBoundaries.Count != 1;

        private void DeactivateCornerClone()
        {
            _cornerClone.Deactivate();
            _clonesQueue.Enqueue(_cornerClone);
        }

        private Clone GetBoundaryClone(in Vector2 direction)
        {
            Clone clone = _clonesQueue.Dequeue();
            _boundaryWithClone[direction] = clone;
            return clone;
        }

        private Clone GetCornerClone()
        {
            _cornerClone = _clonesQueue.Dequeue();
            return _cornerClone;
        }

        private bool IsSecondBoundaryExited() => 
            _intersectedBoundaries.Count != 2;

        private void ActivateBoundaryClone(in Vector2 direction, Clone clone) => 
            clone.Activate(_cloneService.GetClonePosition(direction));

        private void ActivateCornerClone(in Vector2 firstDirection, in Vector2 secondDirection, Clone clone) => 
            clone.Activate(_cloneService.GetCornerClonePosition(firstDirection, secondDirection));
    }

}
