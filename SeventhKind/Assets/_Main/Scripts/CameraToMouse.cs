using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToMouse : MonoBehaviour
{
    public float mouseSensitivity;
    //how much you rotate when moving mouse. 

    public Transform playertarget;

    public float lookClampRange; // max / min range

    float xRotation = 0f; // handles up and down

    public bool inUse;

    void Start()
    {
        TurnOnUse();
    }

    void Update()
    {
        if (inUse)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -lookClampRange, lookClampRange);
            // cannot be above or below a certain range. 
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playertarget.transform.RotateAround(playertarget.transform.position, Vector3.up, mouseX);
        }
    }

    public void TurnOnUse()
    {
        //Turn on mouse cursor
        inUse = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TurnOffUse()
    {
        //Turn off mouse cursor
        inUse = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
