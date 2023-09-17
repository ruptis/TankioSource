using NewTankio.Code.Spawner;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(SpawnPointsGenerator))]
    public class SpawnPointsGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var spawnPointsGenerator = (SpawnPointsGenerator) target;
            if (GUILayout.Button("Generate"))
            {
                spawnPointsGenerator.Generate();
            }
        }
    }
}
