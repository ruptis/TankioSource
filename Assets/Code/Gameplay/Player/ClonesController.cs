using System.Collections.Generic;
using System.Linq;
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
        private IMapBoundaries _mapBoundariesService;

        [Inject]
        public void Construct(IMapBoundaries mapBoundariesService)
        {
            _mapBoundariesService = mapBoundariesService;
            _intersectedBoundaries = new HashSet<Boundary>(_mapBoundariesService.BoundariesCount);
            _boundaryWithClone = new Dictionary<Boundary, Clone>(_mapBoundariesService.BoundariesCount);
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
            
            Debug.Log(gameObject.transform.parent.name + " Entered");
        }

        private void OnBoundaryExited(Boundary boundary)
        {
            _intersectedBoundaries.Add(boundary);
            Vector2 position = transform.position;

            ProcessBoundaryClone(boundary, position, GetBoundaryClone(boundary));

            if (!IsSecondBoundaryExited())
                ProcessCornerClone(_intersectedBoundaries.First(), _intersectedBoundaries.Last(), GetCornerClone());
            
            Debug.Log(gameObject.transform.parent.name + " Exited");
        }

        private void DeactivateCornerClone()
        {
            _cornerClone.Deactivate();
            _clonesQueue.Enqueue(_cornerClone);
        }

        private void DeactivateBoundaryClone(Boundary boundary)
        {
            if (_boundaryWithClone.TryGetValue(boundary, out Clone clone))
                clone.Deactivate();
            _clonesQueue.Enqueue(clone);
        }

        private bool IsSecondBoundaryEntered()
        {
            return _intersectedBoundaries.Count != 1;
        }

        private Clone GetBoundaryClone(Boundary boundary)
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

        private bool IsSecondBoundaryExited()
        {
            return _intersectedBoundaries.Count != 2;
        }

        private void ProcessBoundaryClone(Boundary boundary, Vector2 position, in Clone clone)
        {
            Vector2 closestPoint = boundary.ClosestPoint(position);
            Vector2 oppositeClosestPoint = GetOppositeClosestPoint(boundary, position);
            Vector2 direction = GetDirection(oppositeClosestPoint, closestPoint);
            var distance = Vector2.Distance(oppositeClosestPoint, closestPoint);
            Vector3 clonePosition = GetClonePosition(direction, distance);
            clone.Activate(clonePosition);
        }

        private void ProcessCornerClone(Boundary firstBoundary, Boundary secondBoundary, in Clone clone)
        {
            Vector2 cornerPoint = _mapBoundariesService.GetIntersectionPoint(firstBoundary, secondBoundary);
            Vector2 oppositeCornerPoint = GetOppositeCornerPoint(firstBoundary, secondBoundary);
            Vector2 cornerDirection = GetDirection(oppositeCornerPoint, cornerPoint);
            var distanceToCorner = Vector2.Distance(oppositeCornerPoint, cornerPoint);
            Vector3 cornerPosition = GetClonePosition(cornerDirection, distanceToCorner);
            clone.Activate(cornerPosition);
        }

        private Vector2 GetOppositeClosestPoint(Boundary boundary, Vector2 position)
        {
            Boundary oppositeBoundary = _mapBoundariesService.GetOppositeBoundary(boundary);
            Vector2 oppositeClosestPoint = oppositeBoundary.ClosestPoint(position);
            return oppositeClosestPoint;
        }

        private Vector2 GetOppositeCornerPoint(Boundary firstBoundary, Boundary secondBoundary)
        {
            Boundary firstOppositeBoundary = _mapBoundariesService.GetOppositeBoundary(firstBoundary);
            Boundary secondOppositeBoundary = _mapBoundariesService.GetOppositeBoundary(secondBoundary);
            Vector2 oppositeCornerPoint = _mapBoundariesService.GetIntersectionPoint(firstOppositeBoundary, secondOppositeBoundary);
            return oppositeCornerPoint;
        }
        
        private static Vector3 GetClonePosition(Vector2 direction, float distance) 
            => direction * distance;

        private static Vector2 GetDirection(Vector2 firstPoint, Vector2 secondPoint) 
            => (firstPoint - secondPoint).normalized;
    }

}
