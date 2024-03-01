using NewTankio.Code.Services;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(BoundaryMarker))]
    public sealed class BoundaryMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
        public static void DrawBoundaryMarkerGizmo(BoundaryMarker boundaryMarker, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(boundaryMarker.Position, boundaryMarker.Normal * 10f);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(boundaryMarker.Position, new Vector2(boundaryMarker.Normal.y, -boundaryMarker.Normal.x) * boundaryMarker.VisibleLength);
            Gizmos.DrawRay(boundaryMarker.Position, new Vector2(-boundaryMarker.Normal.y, boundaryMarker.Normal.x) * boundaryMarker.VisibleLength);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var boundaryMarker = (BoundaryMarker)target;
            if (GUILayout.Button("Flip"))
            {
                boundaryMarker.transform.Rotate(0f, 0f, 180f);
            }
        }
    }
}
