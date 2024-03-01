using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public sealed class Boundary
    {
        private Vector2 _absNormal;
        private Vector2 _boundaryPosition;
        private Vector2 _normal;

        public Boundary(Vector2 normal, Vector2 boundaryPosition)
        {
            Normal = normal;
            Position = boundaryPosition;
        }

        public Vector2 Normal
        {
            get => _normal;
            set
            {
                _normal = value.normalized;
                _absNormal = new Vector2(Mathf.Abs(_normal.x), Mathf.Abs(_normal.y));
            }
        }

        public Vector2 Position
        {
            get => _boundaryPosition; 
            set => _boundaryPosition = value;
        }

        public bool Intersects(in Rect rect)
        {
            Vector2 boundsCenterToBoundary = _boundaryPosition - rect.center;
            var projection = Vector2.Dot(boundsCenterToBoundary, _normal);
            var boundsExtentProjection = Vector2.Dot(rect.size * 0.5f, _absNormal);
            return projection < boundsExtentProjection;
        }

        public bool IsOutside(in Vector2 point)
        {
            return GetDistanceToBoundary(point) > 0f;
        }

        public bool IsInside(in Vector2 point)
        {
            return GetDistanceToBoundary(point) <= 0f;
        }

        public Vector2 ClosestPoint(in Vector2 point)
        {
            return point - _normal * GetDistanceToBoundary(point);
        }

        private float GetDistanceToBoundary(in Vector2 point)
        {
            Vector2 pointToBoundary = point - _boundaryPosition;
            return Vector2.Dot(pointToBoundary, _normal);
        }

        public bool TryGetIntersectionPoint(Boundary otherBoundary, out Vector2 intersectionPoint)
        {
            var d1 = new Vector2(-_normal.y, _normal.x);
            var d2 = new Vector2(-otherBoundary._normal.y, otherBoundary._normal.x);

            var denominator = d1.x * d2.y - d1.y * d2.x;
            if (IsParralel(denominator))
            {
                intersectionPoint = Vector2.zero;
                return false;
            }

            var t = ((otherBoundary._boundaryPosition.x - _boundaryPosition.x) * d2.y - (otherBoundary._boundaryPosition.y - _boundaryPosition.y) * d2.x) / denominator;

            intersectionPoint = _boundaryPosition + t * d1;
            return true;
        }

        public bool IsParallel(Boundary otherBoundary)
        {
            var d1 = new Vector2(-_normal.y, _normal.x);
            var d2 = new Vector2(-otherBoundary._normal.y, otherBoundary._normal.x);

            var denominator = d1.x * d2.y - d1.y * d2.x;
            return IsParralel(denominator);
        }
        
        private static bool IsParralel(float denominator, float epsilon = 0.0001f) => 
            Mathf.Abs(denominator) < epsilon;

        public override string ToString() => 
            $"Normal: {_normal}, Position: {_boundaryPosition}";
    }
}
