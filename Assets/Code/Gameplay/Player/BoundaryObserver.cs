using System;
using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class BoundaryObserver : MonoBehaviour
    {
        private IMapBoundaries _mapBoundaries;
        private Boundary[] _boundariesBuffer;
        private HashSet<Boundary> _currentIntersectedBoundaries;
        private HashSet<Boundary> _previousIntersectedBoundaries;

        [SerializeField] private RectAreaMarker _rectArea;

        public event Action<Boundary> BoundaryExited;
        public event Action<Boundary> BoundaryEntered;

        [Inject]
        public void Construct(IMapBoundaries mapBoundaries)
        {
            _mapBoundaries = mapBoundaries;
            _boundariesBuffer = new Boundary[_mapBoundaries.BoundariesCount];
            _currentIntersectedBoundaries = new HashSet<Boundary>(_mapBoundaries.BoundariesCount);
            _previousIntersectedBoundaries = new HashSet<Boundary>(_mapBoundaries.BoundariesCount);
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
            foreach (Boundary boundary in _previousIntersectedBoundaries.Where(boundary => !_currentIntersectedBoundaries.Contains(boundary)))
                BoundaryEntered?.Invoke(boundary);
        }
        
        private void InvokeExited()
        {
            foreach (Boundary boundary in _currentIntersectedBoundaries.Where(boundary => !_previousIntersectedBoundaries.Contains(boundary)))
                BoundaryExited?.Invoke(boundary);
        }

        private void UpdatePreviusSet()
        {
            _previousIntersectedBoundaries.Clear();
            _previousIntersectedBoundaries.UnionWith(_currentIntersectedBoundaries);
        }

        private void UpdateCurrentSet(int count)
        {
            _currentIntersectedBoundaries.Clear();
            for (var i = 0; i < count; i++)
                _currentIntersectedBoundaries.Add(_boundariesBuffer[i]);
        }
    }

}
