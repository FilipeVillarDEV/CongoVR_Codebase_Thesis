using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private float speed;
    //movement speed in units per second
    private float rotationx = 0f;
    private float rotationy = 0f;


    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        rotationx += Input.GetAxis("Mouse X");
        rotationy += -Input.GetAxis("Mouse Y");

        //update the position
        Vector3 forwardInput = verticalInput/50 * forward * speed;
        Vector3 rightInput = horizontalInput/50 * right * speed;
        Vector3 cameraMovement = rightInput + forwardInput;

        transform.Translate(cameraMovement, Space.World);
        //transform.localPosition = transform.forward.normalized  new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, verticalInput * movementSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(rotationy, rotationx, 0);
        //transform.localRotation =  Quaternion.Euler(rotationy, rotationx, 0f);
    }
}
