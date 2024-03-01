using NewTankio.Code.Services;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(MarkerBoundariesGenerator))]
    public sealed class MarkerBoundariesGeneratorEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
        public static void DrawMarkerBoundariesGeneratorGizmo(MarkerBoundariesGenerator markerBoundariesGenerator, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            foreach (Vector2 corner in markerBoundariesGenerator.Corners)
                Gizmos.DrawSphere(corner, 1f);
        }
    }
}
