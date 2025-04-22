    using UnityEngine;
public class CreateMaterials : MonoBehaviour
{
    // Creates a material from shader and texture references.
    [SerializeField] Shader shader;
    [SerializeField] Texture[] textures;
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        Material[] mats = rend.materials;

        for (int i = 0; i <= textures.Length; i++)
        {
            //Renderer rend = gameObject.transform.GetChild(i).
            mats[i] = new Material(shader)
            {
                mainTexture = textures[i]
            };
            rend.materials = mats;
        }
    }
}