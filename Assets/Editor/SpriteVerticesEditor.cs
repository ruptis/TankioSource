using System.Collections.Generic;
using System.Linq;
using NewTankio.Code.Tools;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(SpriteVertices))]
    public class SpriteVerticesEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
        public static void DrawSpriteVerticesGizmo(SpriteVertices spriteVertices, GizmoType gizmoType)
        {
            var spriteRenderer = spriteVertices.GetComponent<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;
            int vertexCount = 0;
            foreach (Vector3 worldVertex in GetVerticesByClockwiseOrder(sprite, spriteVertices.transform).Select(vertex => spriteVertices.transform.TransformPoint(vertex)))
            {
                vertexCount++;
                Gizmos.color = Color.HSVToRGB((float)vertexCount / sprite.vertices.Length, 1, 1);
                Gizmos.DrawSphere((Vector2)worldVertex, spriteVertices.Radius);
            }
        }

        private static List<Vector2> GetVerticesByClockwiseOrder(Sprite sprite, Transform transform)
        {
            var vertices = sprite.vertices;
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
    }
}
