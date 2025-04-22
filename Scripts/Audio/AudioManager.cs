using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Found more than one Audio Manager in the scene");
        Instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }
    /// <summary>
    /// Checks if there is any sound playing in the scene
    /// </summary>
    /// <param name="sound"></param>
    /// <returns></returns>
    public void/*bool*/ CheckIfPlaying()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.getPlaybackState(out PLAYBACK_STATE state);
            if (state == PLAYBACK_STATE.PLAYING)
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        //return false;
    }
    /*public void SetEventParameter(EventReference eventReference, string parameterName, float parameterValue)
    {
        eventReference.setparameterbyname()
    }*/
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }
    private void CleanUp()
    {
        //stop and release any created instances
        foreach(EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        //stop all event emitters, because if we dont they may hang around in another scenes 
        foreach(StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }
    private void OnDestroy()
    {
        CleanUp();
    }
}
