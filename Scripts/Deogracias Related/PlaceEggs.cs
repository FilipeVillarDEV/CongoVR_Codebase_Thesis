using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaceEggs : MonoBehaviour
{
    [SerializeField]
    private GameObject Director_ENG;

    [SerializeField]
    private CanvasGroup GuideCanvas_ENG;
    [SerializeField]
    private CanvasGroup GuideCanvas_PT;

    [SerializeField]
    private GameObject PlaceEggsCanvas_ENG;
    [SerializeField]
    private GameObject PlaceEggsCanvas_PT;

    [SerializeField]
    private GameObject[] Positions;
    [SerializeField]
    private GameObject[] Eggs;

    private readonly Vector3[] OriginalPositions = new Vector3[5];
    private readonly Quaternion[] OriginalRotations = new Quaternion[5];
    private bool reset;

    public bool state;

    private void Start()
    {
        state = reset = false;
        for (int i = 0; i < Eggs.Length; i++)
        {
            OriginalPositions[i] = Eggs[i].transform.position;
            OriginalRotations[i] = Eggs[i].transform.rotation;
        }
    }
    private void Update()
    {
        //verify if art reset is active
        if (ArtistManager.Instance.Reset_Dict["Deogracias"])
        {
            reset = true;
            for (int i = 0; i < Eggs.Length; i++)
            {
                Eggs[i].transform.position = OriginalPositions[i];
                Eggs[i].transform.rotation = OriginalRotations[i];
                Eggs[i].GetComponent<Rigidbody>().freezeRotation = true;
            }
            GetComponent<Collider>().enabled = true;
            ArtistManager.Instance.UpdateArts();
            //Debug.Log("Deogracias -" + ArtistManager.Instance.Reset_Dict["Deogracias"]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            reset = false;

            GetComponent<Collider>().enabled = false;
            if (Director_ENG.activeSelf == true) 
            {
                GuideCanvas_ENG.GetComponentInChildren<TextMeshProUGUI>().text =
                    "Grab the eggs to place them in the nest";
                StartCoroutine(FadeCanvas(GuideCanvas_ENG));
            }
            else
            {
                GuideCanvas_PT.GetComponentInChildren<TextMeshProUGUI>().text =
                    "Agarre nos ovos para os meter no cesto!";
                StartCoroutine(FadeCanvas(GuideCanvas_PT));
            }
        }
    }
    public bool CheckReset()
    {
        return reset;
    }
    public void ReplaceEggs()
    {
        for (int i = 0; i < Eggs.Length; i++)
        {
            Eggs[i].transform.position = Positions[i].transform.position;
        }
        state = true;
    }
    IEnumerator FadeCanvas(CanvasGroup canvas)
    {
        Debug.Log("start waiting");
        yield return new WaitForSeconds(5);
        Debug.Log("stoped waiting");

        for (float alpha = 1f; alpha >= 0; alpha -= 0.005f)
        {
            if (alpha <= 0.0005f)
            {
                alpha = 0;
            }
            yield return canvas.alpha = alpha;
        }
        yield return StartCounting();
    }
    private IEnumerator StartCounting()
    {
        Debug.Log("start waiting");
        yield return new WaitForSeconds(55);
        Debug.Log("stoped waiting");
        if (!state)
        {
            if (Director_ENG.activeSelf == true)
            {
                PlaceEggsCanvas_ENG.SetActive(true);
            }
            else
                PlaceEggsCanvas_PT.SetActive(true);
        }
    }
}
