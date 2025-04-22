using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateArt : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstGazeAudio;
    [SerializeField]
    private PlaceEggs PlaceEggScript;

    private int eggCounter;

    // Start is called before the first frame update
    private void Start()
    {
        eggCounter = 0;
    }
    private void Update()
    {
        if(PlaceEggScript.state)
        {
            FirstGazeAudio.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if collides with grabable object
        if (other.gameObject.layer == 8) 
        {
            eggCounter++;
            Debug.Log("Added egg - " + eggCounter);
            if (eggCounter == 5)
            {
                PlaceEggScript.state = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if grabable leaves nest
        if (other.gameObject.layer == 8)
        {
            eggCounter--;
            Debug.Log("Removed egg - " + eggCounter);
        }
    }
}
    