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
            var vertices = sprite.vertices;
            Gizmos.color = Color.red;
            foreach (Vector3 vertex in vertices)
            {
                Vector3 worldVertex = spriteVertices.transform.TransformPoint(vertex);
                Gizmos.DrawSphere(worldVertex, spriteVertices.Radius);
            }
        }
    }
}
