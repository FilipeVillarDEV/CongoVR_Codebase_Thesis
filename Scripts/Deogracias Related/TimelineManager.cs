using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject director_ENG;

    [SerializeField]
    private GameObject director_PT;

    [SerializeField]
    private CanvasGroup WarningCanvas_ENG;
    [SerializeField]
    private CanvasGroup WarningCanvas_PT;

    [SerializeField]
    private GameObject ActivationCanvas_ENG;

    [SerializeField]
    private GameObject ActivationCanvas_PT;

    private bool warning;
    private void Update()
    {
        if (warning)
        {
            if (director_ENG.activeSelf == true)
            {
                StartCoroutine(FadeCanvas(WarningCanvas_ENG));
            }
            else
                StartCoroutine(FadeCanvas(WarningCanvas_PT));
        }
            
    }

    public void PauseTimeline()
    {
        if (director_ENG.activeSelf == true)
        {
            director_ENG.GetComponent<PlayableDirector>().Pause();
            ActivationCanvas_ENG.SetActive(true);
        }
        else
        {
            director_PT.GetComponent<PlayableDirector>().Pause();
            ActivationCanvas_PT.SetActive(true);
        }

        
    }
    public void StartTimeline()
    {
        if (director_ENG.activeSelf == true)
            director_ENG.GetComponent<PlayableDirector>().Play();
        else
            director_PT.GetComponent<PlayableDirector>().Play();
    }
    public void TimelineEnd()
    {
        if (director_ENG.activeSelf == true)
            WarningCanvas_ENG.GetComponentInChildren<TextMeshProUGUI>().text = "Art piece ended";
        else
            WarningCanvas_PT.GetComponentInChildren<TextMeshProUGUI>().text = "Fim da arte";
        TeleportWarning();
    }
    public void TeleportWarning()
    {
        warning = true;
    }
    IEnumerator FadeCanvas(CanvasGroup canvas)
    {
        for (float alpha = 1f; alpha >= 0; alpha -= 0.005f)
        {
            if (alpha <= 0.0005f)
            {
                alpha = 0;
                warning = false;
            }
            canvas.alpha = alpha;
            yield return null;
        }
    }
}
