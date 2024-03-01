using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public interface IMapBoundaries
    {
        IEnumerable<Boundary> Boundaries { get; }
        int BoundariesCount { get; }

        public int OverlapBoundaries(in Rect rect, IList<Boundary> boundaries);
        public bool IsInside(in Vector2 point);
        public bool IsOutside(in Vector2 point);
        public bool TryGetCrossedBoundary(in Vector2 point, out Boundary boundary);
        Boundary GetOppositeBoundary(in Boundary boundary);
        Vector2 GetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary);
        bool TryGetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary, out Vector2 intersectionPoint);
    }
}
