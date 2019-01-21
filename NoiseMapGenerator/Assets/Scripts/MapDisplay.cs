using UnityEngine;

namespace NoiseGenerator
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer textureRender;

        public void DrawTexture(Texture2D texture)
        {
            // Apply the texture to the referenced object.
            textureRender.sharedMaterial.mainTexture = texture;
            textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }
    }
}