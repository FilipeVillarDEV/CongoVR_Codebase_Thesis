using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

//[RequireComponent(typeof(StudioEventEmitter))]
public class ActivateSound : MonoBehaviour
{
    [SerializeField] 
    private string parameterName;

    [SerializeField]
    private EventReference sound;

    //private float parameterValue;
    private ShowName displayName;
    private int pointerTime;
    private bool paused;

    private GameObject defaultIcon, replayIcon;
    private EventInstance soundActivation;
    public EventInstance SoundActivation { get { return soundActivation; } }
    //private StudioEventEmitter emitter;

    private void Start()
    {
        pointerTime = 0;
        displayName = GetComponent<ShowName>();
        //parameterValue = 1;

        if(transform.childCount > 0)
        {
            defaultIcon = transform.GetChild(0).gameObject;
            replayIcon = transform.GetChild(1).gameObject;
        }

        soundActivation = AudioManager.Instance.CreateEventInstance(sound/*FMODEvents.Instance.LoopSound*/);
        RuntimeManager.AttachInstanceToGameObject(soundActivation, GetComponent<Transform>());

        //emitter = AudioManager.Instance.InitializeEventEmitter(FMODEvents.Instance.SoundActivation, gameObject);
        //emitter.Play();       
    }
    public void PlaySound()
    {
        if (paused)
        {
            soundActivation.setTimelinePosition(pointerTime);
            soundActivation.start();
            paused = !paused;
            Debug.Log("started");   
        }
        //parameterValue = 0;
        //soundActivation.setParameterByName(parameterName, parameterValue);
    }
    public void PauseSound()
    {
        if (!paused)
        {
            soundActivation.getTimelinePosition(out pointerTime);
            soundActivation.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            paused = !paused;
            Debug.Log("paused");
        }
        //parameterValue = 1;
        //soundActivation.setParameterByName(parameterName, parameterValue);
    }
    /// <summary>
    /// IF its a Area Sound activate the sound by entereing the trigger area.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //check if it is the player
        if (other.gameObject.tag == "MainCamera")
        {
            //if there isn't a sound playing play the area sound
            //if (!AudioManager.Instance.CheckIfPlaying())
            //{
            AudioManager.Instance.CheckIfPlaying();
                SoundActivation.start();
                displayName.ActivateUI();

                defaultIcon.SetActive(false);
                replayIcon.SetActive(true);
                Debug.Log("playing Area Sound");
            //}
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            displayName.DeactivateUI();
        }
    }
    private void OnDisable()
    {
        soundActivation.setPaused(true);
        Debug.Log("paused");
    }
}
