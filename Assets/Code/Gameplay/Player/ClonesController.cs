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
        private Dictionary<Boundary, Clone> _boundaryWithClone;
        private Clone _cornerClone;
        private HashSet<Boundary> _intersectedBoundaries;
        private ICloneService _cloneService;

        [Inject]
        public void Construct(IMapBoundaries mapBoundariesService, ICloneService cloneService)
        {
            _cloneService = cloneService;
            _intersectedBoundaries = new HashSet<Boundary>(mapBoundariesService.BoundariesCount);
            _boundaryWithClone = new Dictionary<Boundary, Clone>(mapBoundariesService.BoundariesCount);
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

        private void OnBoundaryEntered(Boundary boundary)
        {
            _intersectedBoundaries.Remove(boundary);

            DeactivateBoundaryClone(boundary);

            if (!IsSecondBoundaryEntered())
                DeactivateCornerClone();
        }

        private void OnBoundaryExited(Boundary boundary)
        {
            _intersectedBoundaries.Add(boundary);

            ActivateBoundaryClone(in boundary, transform.position, GetBoundaryClone(in boundary));

            if (!IsSecondBoundaryExited())
                ActivateCornerClone(_intersectedBoundaries.First(), _intersectedBoundaries.Last(), GetCornerClone());
        }

        private void DeactivateBoundaryClone(Boundary boundary)
        {
            if (_boundaryWithClone.TryGetValue(boundary, out Clone clone))
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

        private Clone GetBoundaryClone(in Boundary boundary)
        {
            Clone clone = _clonesQueue.Dequeue();
            _boundaryWithClone[boundary] = clone;
            return clone;
        }

        private Clone GetCornerClone()
        {
            _cornerClone = _clonesQueue.Dequeue();
            return _cornerClone;
        }

        private bool IsSecondBoundaryExited() => 
            _intersectedBoundaries.Count != 2;

        private void ActivateBoundaryClone(in Boundary boundary, in Vector2 position, Clone clone) => 
            clone.Activate(_cloneService.GetClonePosition(position, boundary));

        private void ActivateCornerClone(in Boundary firstBoundary, in Boundary secondBoundary, Clone clone) => 
            clone.Activate(_cloneService.GetCornerClonePosition(firstBoundary, secondBoundary));
    }

}
