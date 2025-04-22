using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;

public class PlayVideoSound : MonoBehaviour
{
    
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private EventReference sound;
    [SerializeField] private VideoClip source;

    private EventInstance soundActivation;

    void Start()
    {
 
        soundActivation = AudioManager.Instance.CreateEventInstance(sound);
        RuntimeManager.AttachInstanceToGameObject(soundActivation, GetComponent<Transform>());

        videoPlayer.clip = source;
        videoPlayer.Play();
        soundActivation.start();
    }
}
