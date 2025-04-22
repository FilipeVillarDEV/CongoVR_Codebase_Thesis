using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait5seconds : MonoBehaviour
{
    private float timer;
    private bool end;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start waiting");
        timer = 0;
        //StartCoroutine(Wait());
    }
    private void Update()
    {
        if (timer == 120)
        {
            GetComponent<TestPathTrack>().enabled = true;
            Debug.Log("stoped waiting");
            if (end)
                Application.Quit();
        }
        timer++;
    }
    private IEnumerator Wait()
    {
        Debug.Log("start waiting");
        yield return new WaitForSeconds(5);
        GetComponent<TestPathTrack>().enabled = true;
        Debug.Log("stoped waiting");
    }
    public void WaitAndEnd()
    {
        end = true;
        timer = 0;
        //StartCoroutine(End());
    }
    private IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("END");
        Application.Quit();
    }
}
