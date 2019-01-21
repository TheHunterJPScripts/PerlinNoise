using UnityEngine;
using UnityEditor;

namespace NoiseGenerator
{
    [CustomEditor(typeof(MapGenerator2D))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MapGenerator2D mapGen = (MapGenerator2D)target;

            // If autoUpdate is activated, update
            // then the inspector receive any change.
            if (DrawDefaultInspector())
                if (mapGen.autoUpdate)
                    mapGen.GenerateMap();

            // Add a button to manualy generate.
            if (GUILayout.Button("Generate"))
                mapGen.GenerateMap();
        }
    }
}