using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] private float Transparency;
    [SerializeField] private GameObject RayXrInteractor;
    [SerializeField] private Shader shader;
    //private GameObject  lastGameObject;
    private Renderer rend;
    private Texture texture;
    private Shader prevShader;


    // Update is called once per frame
    /*void Update()
    {
        if (RayXrInteractor.GetComponent<XRRayInteractor>().TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            //if its detecting panoramaSlice make it transparent
            if (hit.transform.gameObject.CompareTag("PanoramaSlice"))
            {
                hit.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
                rend = hit.transform.gameObject.GetComponent<Renderer>();

                texture = rend.material.mainTexture;
                prevShader = rend.material.shader;

                rend.material.shader = shader;
                rend.material.SetTexture("_Texture",texture);
                Debug.Log("hit = " + hit.transform.name);

                StartCoroutine(ReactivateColor(hit));
            }            
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PanoramaSlice"))
        {
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            rend = other.gameObject.GetComponent<Renderer>();

            texture = rend.material.mainTexture;
            prevShader = rend.material.shader;

            rend.material.shader = shader;
            rend.material.SetTexture("_Texture", texture);
            Debug.Log("hit = " + other.transform.name);

            StartCoroutine(ReactivateColor(other));
        }
    }
    IEnumerator ReactivateColor(Collider other)
    {
        yield return new WaitForSeconds(3);

        other.gameObject.GetComponent<MeshCollider>().enabled = true;
        other.gameObject.GetComponent<Renderer>().material.shader = prevShader;
    }
}
