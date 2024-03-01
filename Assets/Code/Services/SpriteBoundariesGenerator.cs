using System;
using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
namespace NewTankio.Code.Services
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteBoundariesGenerator : BoundariesGenerator
    {
        public override BoundariesStaticData GenerateBoundariesData()
        {
            var boundariesData = new BoundariesStaticData();
            var vertices = GetVerticesByClockwiseOrder();
            var boundaries = CreateBoundaries(vertices);
            FillWithLocalData(boundariesData, boundaries);
            return boundariesData;
        }

        private List<Vector2> GetVerticesByClockwiseOrder()
        {
            var vertices = GetComponent<SpriteRenderer>().sprite.vertices;
            var verticesCount = vertices.Length;
            Vector3 center = transform.position;
            var verticesWithAngles = new (Vector2 Vertex, float Angle)[verticesCount];

            for (var i = 0; i < verticesCount; i++)
            {
                Vector2 vertex = vertices[i];
                Vector2 worldVertex = transform.TransformPoint(vertex);
                Vector2 direction = (worldVertex - (Vector2)center).normalized;
                var angle = Mathf.Atan2(direction.y, direction.x);
                verticesWithAngles[i] = (Vertex: vertex, Angle: angle);
            }

            return verticesWithAngles.OrderBy(pair => pair.Angle).Select(pair => pair.Vertex).ToList();
        }

        private static List<Boundary> CreateBoundaries(IReadOnlyList<Vector2> vertices)
        {
            var boundaries = new List<Boundary>();
            for (var i = 0; i < vertices.Count; i++)
            {
                Vector2 vertex = vertices[i];
                Vector2 nextVertex = vertices[(i + 1) % vertices.Count];

                Vector2 center = (vertex + nextVertex) / 2;

                Vector2 direction = (nextVertex - vertex).normalized;
                var normal = new Vector2(direction.y, -direction.x);

                boundaries.Add(new Boundary(normal, center));
            }
            return boundaries;
        }

        private static void FillWithLocalData(BoundariesStaticData boundariesData, List<Boundary> boundaries)
        {
            foreach (Boundary boundary in boundaries)
            {
                boundariesData.LocalBoundaryPositions.Add(boundary.Position);
                boundariesData.LocalBoundaryNormals.Add(boundary.Normal);
            }
        }
    }
}
