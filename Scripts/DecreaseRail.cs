using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DecreaseRail : MonoBehaviour
{
    [SerializeField]
    private GameObject Rail;

    private Animator animator;

    private EventInstance soundActivation;

    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        animator = Rail.GetComponent<Animator>();
        hitCount = animator.GetInteger("Hits");
        soundActivation = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.RailingSound);
        RuntimeManager.AttachInstanceToGameObject(soundActivation, GetComponent<Transform>());
    }
    private void OnCollisionEnter(Collision collision)
    {
        ApplyOnHit();
        Debug.Log("collision");
    }
    public void ApplyOnHit()
    {   
        hitCount++;
        Debug.Log(hitCount);
        animator.SetInteger("Hits", hitCount);
        soundActivation.start();
        if (hitCount >= 3)
            Destroy(gameObject);            
    }
}
