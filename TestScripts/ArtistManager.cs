using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ArtistManager : MonoBehaviour
{
    private Dictionary<string, bool> reset_Dict = new Dictionary<string, bool>
    {
            {"Astronauts",false},
            {"Castelie",false},
            {"Deogracias",false},
            {"Hadassa",false},
            {"Lukah",false},
    };

    public Dictionary<string, bool> Reset_Dict { get { return reset_Dict; } }

    public static ArtistManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
            Debug.LogError("Found more than one Art Manager in the scene");
        Instance = this;
    }

    public void ResetArt(string Artist)
    {
        reset_Dict[Artist] = true;
        foreach (KeyValuePair<string, bool> kvp in Reset_Dict)
        {
            Debug.Log(kvp.Key + kvp.Value);
        }
    }
    public void UpdateArts()
    {
        List<string> ListToModify = new();

        foreach (KeyValuePair<string, bool> kvp in Reset_Dict)
        {
            ListToModify.Add(kvp.Key);
        }
        foreach (string key in ListToModify)
        {
            reset_Dict[key] = false;
        }
        ListToModify.Clear();
        Debug.Log("Updated ARTS");
        foreach (KeyValuePair<string, bool> kvp in Reset_Dict)
        {
            Debug.Log(kvp.Key + kvp.Value);
        }
    }
}
