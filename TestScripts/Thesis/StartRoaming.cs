using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StartRoaming : MonoBehaviour
{
    [SerializeField]
    private GameObject Rail;

    [SerializeField]
    private GameObject InvisibleWall;

    private EventInstance releaseSound;
    private Animator animator;

    private readonly float duration = 90.0f;
    private float totalTime = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        releaseSound = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.RailingSound);
        RuntimeManager.AttachInstanceToGameObject(releaseSound, GetComponent<Transform>());
        animator = Rail.GetComponent<Animator>();
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
        //Debug.Log(totalTime);

        if (totalTime > duration)
            startRoaming();
    }
    private void startRoaming()
    {
        //Debug.Log("StartRoaming");

        //Disable Invisible wall
        InvisibleWall.SetActive(false);

        //Play Sound to anounce roaming
        releaseSound.start();

        //Decrease the rail
        animator.SetBool("TimeEnd", true);

        gameObject.GetComponent<StartRoaming>().enabled = false;
    }
}
