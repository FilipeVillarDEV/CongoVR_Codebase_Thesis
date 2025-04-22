using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("FMODEvents")]
    [field: SerializeField] public EventReference CameraActivation { get; private set; }
    [field: SerializeField] public EventReference SoundActivation { get; private set; }
    [field: SerializeField] public EventReference LoopSound { get; private set; }
    [field: SerializeField] public EventReference RailingSound { get; private set; }
    [field: SerializeField] public EventReference SandFootsteps { get; private set; }
    [field: SerializeField] public EventReference WoodFootsteps { get; private set; }

    public static FMODEvents Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Found more than one FMOD Manager in the scene");
        Instance = this;
    }
}
