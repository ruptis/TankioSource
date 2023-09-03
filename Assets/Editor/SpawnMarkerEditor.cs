using DefaultNamespace;
using NewTankio;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
        public static void DrawSpawnMarkerGizmo(SpawnMarker spawnMarker, GizmoType gizmoType)
        {
            var color = spawnMarker.FigureId switch
            {
                FigureId.Square => Color.red,
                FigureId.Triangle => Color.green,
                FigureId.Hexagon => Color.blue,
                _ => Color.white
            };
            Gizmos.color = color;
            Gizmos.DrawSphere(spawnMarker.transform.position, 0.1f);
        }
    }
}
