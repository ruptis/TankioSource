using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public sealed class BoundsMapBoundariesService : IMapBoundaries
    {
        private readonly Boundary[] _boundaries;
        private readonly Dictionary<(Boundary, Boundary), Vector2> _intersectionPoints;
        private readonly Dictionary<Boundary, Boundary> _oppositeBoundaries;

        public BoundsMapBoundariesService(Bounds bounds)
        {
            _boundaries = InitializeBoundaries(bounds);
            _oppositeBoundaries = InitializeOppositeBoundaries(_boundaries);
            _intersectionPoints = InitializeIntersectionPoints(_boundaries);
        }

        public IEnumerable<Boundary> Boundaries => _boundaries;
        public int BoundariesCount => _boundaries.Length;

        public Boundary GetOppositeBoundary(in Boundary boundary)
        {
            return _oppositeBoundaries[boundary];
        }

        public Vector2 GetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary)
        {
            return _intersectionPoints[(boundary, nextBoundary)];
        }

        public bool TryGetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary, out Vector2 intersectionPoint)
        {
            return _intersectionPoints.TryGetValue((boundary, nextBoundary), out intersectionPoint);
        }

        public int OverlapBoundaries(in Rect rect, IList<Boundary> boundaries)
        {
            var count = 0;
            foreach (Boundary boundary in _boundaries)
            {
                if (boundary.Intersects(rect))
                    boundaries[count++] = boundary;
            }

            return count;
        }

        public bool IsInside(in Vector2 point)
        {
            return !IsOutside(point);
        }

        public bool IsOutside(in Vector2 point)
        {
            foreach (Boundary boundary in _boundaries)
            {
                if (boundary.IsOutside(point))
                    return true;
            }

            return false;
        }

        public bool TryGetCrossedBoundary(in Vector2 point, out Boundary boundary)
        {
            boundary = default;
            var distance = float.MaxValue;

            foreach (Boundary b in _boundaries)
            {
                if (!b.IsOutside(point))
                    continue;

                var distanceToBoundary = GetDistance(point, b);
                if (distanceToBoundary > distance)
                    continue;

                distance = distanceToBoundary;
                boundary = b;
            }

            return boundary != default;

            float GetDistance(Vector2 vector2, Boundary b)
            {
                Vector2 pointToBoundary = b.Position - vector2;
                var distanceToBoundary = pointToBoundary.sqrMagnitude;
                return distanceToBoundary;
            }
        }

        private static Boundary[] InitializeBoundaries(Bounds bounds)
        {
            var left = new Vector2(bounds.min.x, bounds.min.y + bounds.extents.y);
            var right = new Vector2(bounds.max.x, bounds.min.y + bounds.extents.y);
            var bottom = new Vector2(bounds.min.x + bounds.extents.x, bounds.min.y);
            var top = new Vector2(bounds.min.x + bounds.extents.x, bounds.max.y);

            return new Boundary[]
            {
                new(Vector2.left, left),
                new(Vector2.down, bottom),
                new(Vector2.right, right),
                new(Vector2.up, top)
            };
        }
        private static Dictionary<Boundary, Boundary> InitializeOppositeBoundaries(IReadOnlyList<Boundary> boundaries)
        {
            var oppositeBoundaries = new Dictionary<Boundary, Boundary>();
            for (var i = 0; i < boundaries.Count; i++)
            {
                Boundary boundary = boundaries[i];
                Boundary oppositeBoundary = boundaries[(i + 2) % boundaries.Count];
                oppositeBoundaries.Add(boundary, oppositeBoundary);
            }

            return oppositeBoundaries;
        }
        private static Dictionary<(Boundary, Boundary), Vector2> InitializeIntersectionPoints(IReadOnlyList<Boundary> boundaries)
        {
            var intersectionPoints = new Dictionary<(Boundary, Boundary), Vector2>();

            for (var i = 0; i < boundaries.Count; i++)
            {
                Boundary boundary = boundaries[i];
                Boundary nextBoundary = boundaries[(i + 1) % boundaries.Count];

                if (!boundary.TryGetIntersectionPoint(nextBoundary, out Vector2 cornerPoint))
                    continue;
                intersectionPoints.Add((boundary, nextBoundary), cornerPoint);
                intersectionPoints.Add((nextBoundary, boundary), cornerPoint);
            }

            return intersectionPoints;
        }
    }
}
