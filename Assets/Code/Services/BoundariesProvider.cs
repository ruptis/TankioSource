using System;
using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
using VContainer.Unity;
namespace NewTankio.Code.Services
{
    public sealed class BoundariesProvider : IBoundariesProvider, ITickable
    {
        private readonly ITransformProvider _mapTransformProvider;

        private readonly List<Boundary> _boundaries;
        private readonly List<Vector3> _localBoundaryPositions;
        private readonly List<Vector3> _localBoundaryNormals;
        private readonly Vector3[] _positionsBuffer;
        private readonly Vector3[] _normalsBuffer;

        public BoundariesProvider(ITransformProvider mapTransformProvider, BoundariesStaticData boundariesStaticData)
        {
            _mapTransformProvider = mapTransformProvider;
            _localBoundaryPositions = new List<Vector3>(boundariesStaticData.LocalBoundaryPositions);
            _localBoundaryNormals = new List<Vector3>(boundariesStaticData.LocalBoundaryNormals);
            _boundaries = new List<Boundary>(_localBoundaryPositions.Count);
            for (var i = 0; i < _localBoundaryPositions.Count; i++)
                _boundaries.Add(new Boundary(_localBoundaryNormals[i], _localBoundaryPositions[i]));
            
            _positionsBuffer = new Vector3[_localBoundaryPositions.Count];
            _normalsBuffer = new Vector3[_localBoundaryNormals.Count];
            
            UpdateBoundaries();
            
            var oppositeBoundaries = _boundaries.ToDictionary(boundary => boundary, 
                boundary => _boundaries.First(b => b.IsParallel(boundary) && !b.Equals(boundary)));
            
            var intersectionPoints = new Dictionary<(Boundary, Boundary), Vector2>();
            for (var i = 0; i < _boundaries.Count; i++)
            {
                Boundary boundary = _boundaries[i];
                Boundary nextBoundary = _boundaries[(i + 1) % _boundaries.Count];
                if (boundary.TryGetIntersectionPoint(nextBoundary, out Vector2 intersectionPoint))
                {
                    intersectionPoints.Add((boundary, nextBoundary), intersectionPoint);
                    intersectionPoints.Add((nextBoundary, boundary), intersectionPoint);
                }
            }
            
            if (intersectionPoints.Count != _boundaries.Count * 2)
                throw new InvalidOperationException("Boundary markers should be placed in a circle");

            OppositeBoundaries = oppositeBoundaries;
            IntersectionPoints = intersectionPoints;
        }

        public IReadOnlyList<Boundary> Boundaries => _boundaries;
        public IReadOnlyDictionary<Boundary, Boundary> OppositeBoundaries { get; }
        public IReadOnlyDictionary<(Boundary, Boundary), Vector2> IntersectionPoints { get; }

        private void UpdateBoundaries()
        {
            _localBoundaryPositions.CopyTo(_positionsBuffer);
            _localBoundaryNormals.CopyTo(_normalsBuffer);

            _mapTransformProvider.Transform.TransformPoints(_positionsBuffer);
            _mapTransformProvider.Transform.TransformVectors(_normalsBuffer);

            for (var i = 0; i < _boundaries.Count; i++)
            {
                _boundaries[i].Normal = _normalsBuffer[i];
                _boundaries[i].Position = _positionsBuffer[i];
            }
        }
        
        public void Tick()
        {
            if (!_mapTransformProvider.Transform.hasChanged)
                return;
            
            UpdateBoundaries();
            _mapTransformProvider.Transform.hasChanged = false;
        }
    }
}
