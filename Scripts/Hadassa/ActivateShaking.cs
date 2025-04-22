using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShaking : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if reset activated do:
        if (ArtistManager.Instance.Reset_Dict["Hadassa"])
        {
            animator.SetBool("Entered", false);
            GetComponent<Collider>().enabled = true;
            GetComponent<ActivateSound>().PauseSound();
            ArtistManager.Instance.UpdateArts();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            animator.SetBool("Entered", true);
            GetComponent<Collider>().enabled = false;
        }
    }
    
}
