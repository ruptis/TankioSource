using NewTankio.Code.Services;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(MapData))]
    public class MapDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mapData = (MapData)target;
            if (GUILayout.Button("Generate Boundaries"))
            {
                var boundariesGenerator = FindObjectOfType<BoundariesGenerator>();
                mapData.BoundariesStaticData = boundariesGenerator.GenerateBoundariesData();
            }
            EditorUtility.SetDirty(mapData);
        }
    }
}
