using System;
using System.Collections.Generic;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class BoundaryObserver : MonoBehaviour
    {
        private IMapBoundaries _mapBoundaries;
        private Vector2[] _directionsBuffer;
        private List<Vector2> _currentIntersectedBoundariesDirections;
        private List<Vector2> _previousIntersectedBoundariesDirections;
        private HashSet<Vector2> _tempHashSet;

        [SerializeField] private RectAreaMarker _rectArea;

        public event Action<Vector2> BoundaryExited;
        public event Action<Vector2> BoundaryEntered;

        [Inject]
        public void Construct(IMapBoundaries mapBoundaries)
        {
            _mapBoundaries = mapBoundaries;
            _directionsBuffer = new Vector2[_mapBoundaries.BoundariesCount];
            _currentIntersectedBoundariesDirections = new List<Vector2>(_mapBoundaries.BoundariesCount);
            _previousIntersectedBoundariesDirections = new List<Vector2>(_mapBoundaries.BoundariesCount);
            _tempHashSet = new HashSet<Vector2>(_mapBoundaries.BoundariesCount);
        }
        
        private void LateUpdate()
        {
            var count = _mapBoundaries.OverlapBoundaries(_rectArea.Rect, _directionsBuffer);
            
            UpdateCurrentSet(count);
            InvokeEntered();
            InvokeExited();
            UpdatePreviusSet();
        }

        private void InvokeEntered()
        {
            _tempHashSet.Clear();
            _tempHashSet.UnionWith(_previousIntersectedBoundariesDirections);
            _tempHashSet.ExceptWith(_currentIntersectedBoundariesDirections);
            
            foreach (Vector2 direction in _tempHashSet)
                BoundaryEntered?.Invoke(direction);
        }
        
        private void InvokeExited()
        {
            _tempHashSet.Clear();
            _tempHashSet.UnionWith(_currentIntersectedBoundariesDirections);
            _tempHashSet.ExceptWith(_previousIntersectedBoundariesDirections);

            foreach (Vector2 direction in _tempHashSet)
                BoundaryExited?.Invoke(direction);
        }

        private void UpdatePreviusSet()
        {
            _previousIntersectedBoundariesDirections.Clear();
            _previousIntersectedBoundariesDirections.AddRange(_currentIntersectedBoundariesDirections);
        }

        private void UpdateCurrentSet(int count)
        {
            _currentIntersectedBoundariesDirections.Clear();
            for (var i = 0; i < count; i++)
                _currentIntersectedBoundariesDirections.Add(_directionsBuffer[i]);
        }
    }

}
