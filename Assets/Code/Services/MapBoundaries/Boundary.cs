﻿using UnityEngine;
namespace NewTankio.Code.Services
{
    public readonly struct Boundary
    {
        private readonly Vector2 _normal;
        private readonly Vector2 _absNormal;
        private readonly Vector2 _boundaryPosition;

        public Vector2 Normal => _normal;
        public Vector2 Position => _boundaryPosition;

        public Boundary(Vector2 normal, Vector2 boundaryPosition)
        {
            _normal = normal.normalized;
            _absNormal = new Vector2(Mathf.Abs(_normal.x), Mathf.Abs(_normal.y));
            _boundaryPosition = boundaryPosition;
        }

        public bool Intersects(in Bounds bounds)
        {
            Vector2 boundsCenterToBoundary = _boundaryPosition - (Vector2)bounds.center;
            var projection = Vector2.Dot(boundsCenterToBoundary, _normal);
            var boundsExtentProjection = Vector2.Dot(bounds.extents, _absNormal);
            return projection < boundsExtentProjection;
        }

        public Vector2 ClosestPoint(in Vector2 point)
        {
            Vector2 pointToBoundary = point - _boundaryPosition;
            var distanceToBoundary = Vector2.Dot(pointToBoundary, _normal);
            return point - _normal * distanceToBoundary;
        }
        
        public bool TryGetIntersectionPoint(in Boundary otherBoundary, out Vector2 intersectionPoint)
        {
            Vector2 p1 = _boundaryPosition;
            Vector2 p2 = otherBoundary._boundaryPosition;

            var d1 = new Vector2(-_normal.y, _normal.x);
            var d2 = new Vector2(-otherBoundary._normal.y, otherBoundary._normal.x);

            var denominator = d1.x * d2.y - d1.y * d2.x;
            if (Mathf.Abs(denominator) < Mathf.Epsilon)
            {
                intersectionPoint = Vector2.zero;
                return false;
            }

            var t = ((p2.x -p1.x) * d2.y - (p2.y - p1.y) * d2.x) / denominator;

            intersectionPoint = p1 + t * d1;
            return true;
        }
    }
}