using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public interface IMapBoundaries
    {
        public IEnumerable<Boundary> Boundaries { get; }
        public int BoundariesCount { get; }
        public Vector2 MinPoint { get; }
        public Vector2 MaxPoint { get; }
        
        Boundary GetOppositeBoundary(in Boundary boundary);
        Vector2 GetIntersectionPoint(in Boundary boundary, in Boundary nextBoundary);
        public int OverlapBoundaries(in Rect rect, IList<Boundary> boundaries);
        public bool IsInside(in Vector2 point);
        public bool IsOutside(in Vector2 point);
        public bool TryGetCrossedBoundary(in Vector2 point, out Boundary boundary);
    }
}
