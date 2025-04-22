using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSequence : MonoBehaviour
{
    [SerializeField]
    private PlaceEggs PlaceEggsScript;

    [SerializeField] 
    private Transform[] Positions;

    [SerializeField]
    private GameObject[] HighLightList;

    private int sequence;
    // Start is called before the first frame update
    void Start()
    {
        sequence = 0;
    }
    private void Update()
    {
        //verify if art reset is active
        if (PlaceEggsScript.CheckReset())
        {
            ResetSequence();
        }
    }

    public void Teleport()
    {
        Debug.Log("PRESSED");
        transform.position = Positions[sequence].position;
        Camera.main.transform.forward = Positions[sequence].forward;
        sequence++;
        ActivateHighLights();

        if (sequence > 4)
            ResetSequence();
    }
    public void ActivateHighLights()
    {
        //activate spotlight
        HighLightList[sequence].SetActive(true);

        //Deactivate previous spotlight
        if (sequence == 0)
        {
            HighLightList[HighLightList.Length - 1].SetActive(false);
        }
        else
            HighLightList[sequence - 1].SetActive(false);

    }
    public void ResetSequence()
    {
        sequence = 0;
        foreach (GameObject obj in HighLightList) 
        { 
            obj.SetActive(false);
        }
    }
}
