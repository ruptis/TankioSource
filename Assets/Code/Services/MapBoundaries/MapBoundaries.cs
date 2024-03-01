using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public class MapBoundaries : IMapBoundaries
    {
        private readonly IBoundariesProvider _boundariesProvider;

        public MapBoundaries(IBoundariesProvider boundariesProvider) => 
            _boundariesProvider = boundariesProvider;

        public IEnumerable<Boundary> Boundaries => _boundariesProvider.Boundaries;
        public int BoundariesCount => _boundariesProvider.Boundaries.Count;

        public int OverlapBoundaries(in Rect rect, IList<Boundary> boundaries)
        {
            var count = 0;
            foreach (Boundary boundary in Boundaries)
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
            foreach (Boundary boundary in Boundaries)
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

            foreach (Boundary b in Boundaries)
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

        public Boundary GetOppositeBoundary(in Boundary boundary) => 
            _boundariesProvider.OppositeBoundaries[boundary];

        public Vector2 GetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary) => 
            _boundariesProvider.IntersectionPoints[(boundary, nextBoundary)];

        public bool TryGetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary, out Vector2 intersectionPoint) => 
            _boundariesProvider.IntersectionPoints.TryGetValue((boundary, nextBoundary), out intersectionPoint);
    }
}
