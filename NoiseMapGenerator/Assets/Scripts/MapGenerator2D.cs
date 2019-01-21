using UnityEngine;

namespace NoiseGenerator {
    [RequireComponent(typeof(MapDisplay))]
    public class MapGenerator2D : MonoBehaviour {
        public enum DrawMode { NoiseMap, ColourMap };

        [Header("General Settings:")]
        [Tooltip("Update the data instantly.")]
        public bool autoUpdate = false;
        public DrawMode drawMode = DrawMode.NoiseMap;

        [Space(5)]

        public int mapWidth = 100;
        public int mapHeight = 100;

        [Space(5)]

        public int seed = 0;

        [Header("Noise Settings:")]
        [Range(1, 10)]
        public int octaves = 8;
        [Range(0, 1)]
        [Tooltip("Smoothness of the land.")]
        public float persistance = 0.8f;
        [Range(0, 2)]
        [Tooltip("Land details.")]
        public float lacunarity = 1.3f;
        public float noiseScale = 20.0f;

        [Space(5)]

        public Vector2 offset = new Vector2(1000.0f, 1000.0f);

        [Header("Colors:")]
        public TerrainType[] regions;

        public void GenerateMap() {
            // Reference to the display script.
            MapDisplay display = GetComponent<MapDisplay>();
            // Generate the noise height.
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

            if (drawMode == DrawMode.ColourMap) {
                // Throw warning if list is empty.
                if (regions.Length == 0) {
                    Debug.LogWarning("The list of color in [" + gameObject.name + "/MapGenerator2D] is empty");
                }

                Color[] colorMap = new Color[mapWidth * mapHeight];
                // Stores the color that maches eeach height on colorMap.
                for (int y = 0; y < mapHeight; y++) {
                    for (int x = 0; x < mapWidth; x++) {
                        float currentHeight = noiseMap[x, y];
                        for (int i = 0; i < regions.Length; i++) {
                            if (currentHeight <= regions[i].height) {
                                colorMap[y * mapWidth + x] = regions[i].colour;
                                break;
                            }
                        }
                    }
                }
                // Display the map.
                display.DrawTexture(TextureGenerator.TextureFromColourMap(colorMap, mapWidth, mapHeight));
            } else {
                // Display the map.
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
            }
        }

        private void OnValidate() {
            // We make sure that none of
            // these variables can have values that 
            // will cause error by wrong usage.
            if (mapWidth < 1)
                mapWidth = 1;
            if (mapHeight < 1)
                mapHeight = 1;
            if (lacunarity < 1)
                lacunarity = 1;
            if (octaves < 0)
                octaves = 0;
        }
    }

    [System.Serializable]
    public struct TerrainType {
        [Range(0, 1)]
        public float height;
        public Color colour;
    }
}