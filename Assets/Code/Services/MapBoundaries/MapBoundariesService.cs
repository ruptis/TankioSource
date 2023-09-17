using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Services
{
    public class MapBoundariesService : IMapBoundaries
    {
        private readonly Boundary[] _boundaries;
        private readonly Dictionary<Boundary, Boundary> _oppositeBoundaries;
        private readonly Dictionary<(Boundary, Boundary), Vector2> _intersectionPoints;

        public MapBoundariesService(Bounds bounds)
        {
            MinPoint = bounds.min;
            MaxPoint = bounds.max;
            _boundaries = InitializeBoundaries(bounds);
            _oppositeBoundaries = InitializeOppositeBoundaries(_boundaries);
            _intersectionPoints = InitializeIntersectionPoints(_boundaries);
        }
        
        public IEnumerable<Boundary> Boundaries => _boundaries;
        public int BoundariesCount => _boundaries.Length;
        public Vector2 MinPoint { get; }
        public Vector2 MaxPoint { get; }
        

        public Boundary GetOppositeBoundary(in Boundary boundary)
        {
            return _oppositeBoundaries[boundary];
        }

        public Vector2 GetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary)
        {
            return _intersectionPoints[(boundary, nextBoundary)];
        }
        
        public int OverlapBoundaries(in Bounds bounds, IList<Boundary> boundaries)
        {
            var count = 0;
            foreach (Boundary boundary in _boundaries)
                if (boundary.Intersects(bounds))
                    boundaries[count++] = boundary;

            return count;
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
