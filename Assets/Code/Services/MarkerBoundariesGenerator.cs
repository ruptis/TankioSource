using System;
using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services
{
    public sealed class MarkerBoundariesGenerator : BoundariesGenerator
    {
        public List<BoundaryMarker> BoundaryMarkers;

        public List<Vector2> Corners;

        [ContextMenu("Collect Markers")]
        public void CollectMarkers()
        {
            BoundaryMarkers = GetComponentsInChildren<BoundaryMarker>().ToList();
            
            if (BoundaryMarkers.Count < 4)
                throw new InvalidOperationException("At least 4 boundary markers should be placed");

            var boundaries = CreateBoundaries();
            SortByClockwiseOrder(boundaries);
            var oppositeBoundaries = CreateOppositeBoundaries(boundaries);
            if (oppositeBoundaries.Count != BoundaryMarkers.Count || oppositeBoundaries.Any(pair => pair.Value == null))
                throw new InvalidOperationException("Boundary markers should be placed in a circle");
            
            var intersectionPoints = CreateIntersectionPoints(boundaries);
            if (intersectionPoints.Count < BoundaryMarkers.Count * 2)
                throw new InvalidOperationException("Boundary markers should be placed in a circle");
            
            Corners = intersectionPoints.Values.ToList();
        }
        
        private static void SortByClockwiseOrder(List<Boundary> boundaries)
        {
            Vector2 center = boundaries.Aggregate(Vector2.zero, (current, boundary) => current + boundary.Position);
            center /= boundaries.Count;
            boundaries.Sort((b1, b2) => 
                Mathf.Atan2(b1.Position.y - center.y, b1.Position.x - center.x)
                    .CompareTo(Mathf.Atan2(b2.Position.y - center.y, b2.Position.x - center.x)));
        }

        public override BoundariesStaticData GenerateBoundariesData()
        {
            var boundariesData = new BoundariesStaticData();
            var boundaries = CreateBoundaries();
            SortByClockwiseOrder(boundaries);
            FillWithLocalData(boundariesData, boundaries);
            return boundariesData;
        }

        private List<Boundary> CreateBoundaries() => 
            BoundaryMarkers.ConvertAll(marker => new Boundary(marker.Normal, marker.Position));
        
        private void FillWithLocalData(BoundariesStaticData boundariesData, List<Boundary> boundaries)
        {
            foreach (Boundary boundary in boundaries)
            {
                boundariesData.LocalBoundaryPositions.Add(transform.InverseTransformPoint(boundary.Position));
                boundariesData.LocalBoundaryNormals.Add(boundary.Normal);
            }
        }
        
        private static Dictionary<Boundary, Boundary> CreateOppositeBoundaries(IReadOnlyCollection<Boundary> boundaries) => 
            boundaries.ToDictionary(boundary => boundary, 
            boundary => boundaries.FirstOrDefault(b => b.IsParallel(boundary) && !b.Equals(boundary)));
        
        private static Dictionary<(Boundary, Boundary), Vector2> CreateIntersectionPoints(IReadOnlyList<Boundary> boundaries)
        {
            var intersectionPoints = new Dictionary<(Boundary, Boundary), Vector2>();
            for (var i = 0; i < boundaries.Count; i++)
            {
                Boundary boundary = boundaries[i];
                Boundary nextBoundary = boundaries[(i + 1) % boundaries.Count];
                if (boundary.TryGetIntersectionPoint(nextBoundary, out Vector2 intersectionPoint))
                {
                    intersectionPoints.Add((boundary, nextBoundary), intersectionPoint);
                    intersectionPoints.Add((nextBoundary, boundary), intersectionPoint);
                }
            }
            return intersectionPoints;
        }
    }
}
