using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisableTeleport : MonoBehaviour
{
    [SerializeField]
    private UnityEvent disableRightHandTeleport;

    [SerializeField]
    private UnityEvent enableRightHandTeleport;

    [SerializeField]
    private Canvas canvas;
    // Start is called before the first frame update
    void OnEnable()
    {
        disableRightHandTeleport.Invoke();
        canvas.enabled = true;
    }
    public void ActivateTeleport()
    {
        enableRightHandTeleport.Invoke();
        enabled = false;
    }
}
