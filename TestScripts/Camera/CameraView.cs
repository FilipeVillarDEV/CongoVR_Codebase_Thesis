using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR;

public class CameraView : MonoBehaviour
{
    [SerializeField]
    private int Layer;

    [SerializeField]
    private float DetectionRadius;

    private int layerMask, distance;
    private Camera cam;
    private GameObject currentGameObject, lastGameObject;
    private bool visible;

    void Start()
    {
        cam = GetComponent<Camera>();
        layerMask = 1 << Layer;
        distance = 20;
    }

    void Update()
    {
        RaycastHit hit;
        Ray centerRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //if any of the Rays hit a sound object Play Sound  
        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(centerRay, DetectionRadius, out hit, distance, 
            layerMask, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor))
        {
            //Debug.Log("I'm looking at " + hit.transform.name);
            currentGameObject = hit.transform.gameObject;

            //check if it has the ActivateSound Script
            if (currentGameObject.GetComponent<ActivateSound>() == null)
                Debug.LogError("The GameObject" + hit.transform.name +"doesn´t have the ActivateSound Script Component!" );
            //check if it has the ShowName Script
            if (currentGameObject.GetComponent<ShowName>() == null)
                Debug.LogError("The GameObject" + hit.transform.name + "doesn´t have the ShowName Script Component!");

            if (lastGameObject != null)
            {
                currentGameObject.GetComponent<ActivateSound>().PlaySound();
                if (!visible)
                {
                    currentGameObject.GetComponent<ShowName>().ActivateUI();
                    visible = true;
                }
            }

            lastGameObject = currentGameObject;
        }
        else
        {
            //Debug.Log("I'm looking at nothing!");
            if(lastGameObject != null && lastGameObject.GetComponent<ActivateSound>() != null)
            {
                lastGameObject.GetComponent<ActivateSound>().PauseSound();
                if(visible)
                {
                    lastGameObject.GetComponent<ShowName>().DeactivateUI();
                    visible = false;    
                }
            }
        }
    }
}
