using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartDancing : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if reset activated do:
        if (ArtistManager.Instance.Reset_Dict["Castelie"])
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
