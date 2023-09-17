﻿using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Services;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public class ClonesController : MonoBehaviour
    {
        private const int MaxClones = 3;

        [SerializeField] private BoundaryObserver _boundaryObserver;
        [SerializeField] private SpriteRenderer[] _cloneSprites = new SpriteRenderer[MaxClones];

        private readonly Queue<Clone> _clones = new(MaxClones);
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
            foreach (SpriteRenderer spriteRenderer in _cloneSprites)
                _clones.Enqueue(new Clone(spriteRenderer));
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
            Vector2 position = transform.position;

            ProcessBoundaryClone(boundary, position, GetBoundaryClone(boundary));

            if (!IsSecondBoundaryExited())
                ProcessCornerClone(_intersectedBoundaries.First(), _intersectedBoundaries.Last(), position, GetCornerClone());
        }

        private void DeactivateCornerClone()
        {
            _cornerClone.Deactivate();
            _clones.Enqueue(_cornerClone);
        }

        private void DeactivateBoundaryClone(Boundary boundary)
        {
            if (_boundaryWithClone.TryGetValue(boundary, out Clone clone))
                clone.Deactivate();
            _clones.Enqueue(clone);
        }

        private bool IsSecondBoundaryEntered()
        {
            return _intersectedBoundaries.Count != 1;
        }

        private Clone GetBoundaryClone(Boundary boundary)
        {
            Clone clone = _clones.Dequeue();
            _boundaryWithClone[boundary] = clone;
            return clone;
        }

        private Clone GetCornerClone()
        {
            _cornerClone = _clones.Dequeue();
            return _cornerClone;
        }

        private bool IsSecondBoundaryExited()
        {
            return _intersectedBoundaries.Count != 2;
        }

        private void ProcessBoundaryClone(Boundary boundary, Vector2 position, in Clone clone)
        {
            Vector2 closestPoint = boundary.ClosestPoint(position);
            var distance = Vector2.Distance(position, closestPoint);
            Vector2 oppositeClosestPoint = GetOppositeClosestPoint(boundary, position);
            Vector2 direction = GetDirection(oppositeClosestPoint, closestPoint);
            Vector3 cloneLocalPosition = GetCloneLocalPosition(oppositeClosestPoint, direction, distance);
            clone.Activate(cloneLocalPosition);
        }

        private void ProcessCornerClone(Boundary firstBoundary, Boundary secondBoundary, Vector2 position, in Clone clone)
        {
            Vector2 cornerPoint = _mapBoundariesService.GetIntersectionPoint(firstBoundary, secondBoundary);
            var distanceToCorner = Vector2.Distance(position, cornerPoint);
            Vector2 oppositeCornerPoint = GetOppositeCornerPoint(firstBoundary, secondBoundary);
            Vector2 cornerDirection = GetDirection(oppositeCornerPoint, cornerPoint);
            Vector3 cornerLocalPosition = GetCloneLocalPosition(oppositeCornerPoint, cornerDirection, distanceToCorner);
            clone.Activate(cornerLocalPosition);
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

        private Vector3 GetCloneLocalPosition(Vector2 point, Vector2 direction, float distance)
        {
            Vector2 cloneWorldPosition = point + direction * distance;
            Vector3 cloneLocalPosition = transform.InverseTransformPoint(cloneWorldPosition);
            return cloneLocalPosition;
        }

        private static Vector2 GetDirection(Vector2 firstPoint, Vector2 secondPoint)
        {
            Vector2 direction = (firstPoint - secondPoint).normalized;
            return direction;
        }
    }

}