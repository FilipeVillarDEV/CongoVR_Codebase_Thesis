using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowActivator : MonoBehaviour
{
    private Material baseMaterial;

    private void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
    }
    public void ActivateGlow()
    {
        baseMaterial.SetInt("_Glow", 1);
    }
    public void DisableGlow()
    {
        baseMaterial.SetInt("_Glow", 0);
    }
}
