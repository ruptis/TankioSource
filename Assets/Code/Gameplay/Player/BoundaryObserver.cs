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
        private Boundary[] _boundariesBuffer;
        private List<Boundary> _currentIntersectedBoundaries;
        private List<Boundary> _previousIntersectedBoundaries;
        private HashSet<Boundary> _tempHashSet;

        [SerializeField] private RectAreaMarker _rectArea;

        public event Action<Boundary> BoundaryExited;
        public event Action<Boundary> BoundaryEntered;

        [Inject]
        public void Construct(IMapBoundaries mapBoundaries)
        {
            _mapBoundaries = mapBoundaries;
            _boundariesBuffer = new Boundary[_mapBoundaries.BoundariesCount];
            _currentIntersectedBoundaries = new List<Boundary>(_mapBoundaries.BoundariesCount);
            _previousIntersectedBoundaries = new List<Boundary>(_mapBoundaries.BoundariesCount);
            _tempHashSet = new HashSet<Boundary>(_mapBoundaries.BoundariesCount);
        }
        
        private void LateUpdate()
        {
            var count = _mapBoundaries.OverlapBoundaries(_rectArea.Rect, _boundariesBuffer);
            
            UpdateCurrentSet(count);
            InvokeEntered();
            InvokeExited();
            UpdatePreviusSet();
        }

        private void InvokeEntered()
        {
            _tempHashSet.Clear();
            _tempHashSet.UnionWith(_previousIntersectedBoundaries);
            _tempHashSet.ExceptWith(_currentIntersectedBoundaries);
            
            foreach (Boundary boundary in _tempHashSet)
                BoundaryEntered?.Invoke(boundary);
        }
        
        private void InvokeExited()
        {
            _tempHashSet.Clear();
            _tempHashSet.UnionWith(_currentIntersectedBoundaries);
            _tempHashSet.ExceptWith(_previousIntersectedBoundaries);

            foreach (Boundary boundary in _tempHashSet)
                BoundaryExited?.Invoke(boundary);
        }

        private void UpdatePreviusSet()
        {
            _previousIntersectedBoundaries.Clear();
            _previousIntersectedBoundaries.AddRange(_currentIntersectedBoundaries);
        }

        private void UpdateCurrentSet(int count)
        {
            _currentIntersectedBoundaries.Clear();
            for (var i = 0; i < count; i++)
                _currentIntersectedBoundaries.Add(_boundariesBuffer[i]);
        }
    }

}
