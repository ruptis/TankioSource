using System.Collections.Generic;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services
{
    public interface IBoundariesProvider
    {
        public IReadOnlyList<Boundary> Boundaries { get; }
        
        public IReadOnlyDictionary<Boundary, Boundary> OppositeBoundaries { get; }
        public IReadOnlyDictionary<(Boundary, Boundary), Vector2> IntersectionPoints { get; }
    }
}
