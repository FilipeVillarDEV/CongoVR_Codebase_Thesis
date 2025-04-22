using System.Collections;
using UnityEngine;

public class TestPathTrack : MonoBehaviour
{
    private wait5seconds wait5seconds;
    private bool rotate, end, completed;
    private float degree, rotationDegree, currentPosition, translation;
    private string filename;
    // Start is called before the first frame update
    void Start()
    {
        
        filename = "Photo";
        rotate = true;
        completed = false;
        end = false;
        rotationDegree = 0.5f;
        translation = 0.05f;
        wait5seconds = GetComponent<wait5seconds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            rotate = Rotate(end);

        else if (!rotate)
        {
            completed = MoveForward();
        }
        if (completed)
        {
            rotate = true;
            end = true;
        }
    }
    private bool Rotate(bool end)
    {
        degree += rotationDegree;
        transform.Rotate(0, rotationDegree, 0);
        if (degree >= 360)
        {
            if (end)
            {
                ScreenCapture.CaptureScreenshot(filename + 02 + ".png");
                Debug.Log("Took SCREENSHOT");
                wait5seconds.WaitAndEnd();
                GetComponent<TestPathTrack>().enabled = false;
            }
            else
            {
                ScreenCapture.CaptureScreenshot(filename + 01 + ".png");
                degree = 0;
                return false;
            }
        }

        return true;
    }
    private bool MoveForward()
    {
        currentPosition += translation;
        transform.Translate(0, 0, translation);

        if (currentPosition >= 12f)
            return true;

        return false;
    }
}
