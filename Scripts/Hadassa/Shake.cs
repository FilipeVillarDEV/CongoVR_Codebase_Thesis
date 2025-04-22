using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [Range(0,100f)]
    public float speed; //how fast it shakes

    [Range(0,0.5f)]
    public float amount; //how much it shakes

    private Vector3 position;

    void Update()
    {
        position = transform.position;
        //position.x = position.x + Mathf.Sin(Time.time * speed) * amount;
        position.z = position.z + Mathf.Sin(Time.time * speed) * amount;
        transform.position = position;
    }
}
