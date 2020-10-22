using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToMouse : MonoBehaviour
{
    // Liam 

    public float mouseSensitivity;
    //how much you rotate when moving mouse. 

    public Transform playertarget;

    public float lookClampRange; // max / min range

    float xRotation = 0f; // handles up and down

    public bool inUse;

    void Start()
    {
        TurnOn();
        // Cursor doesn't move.
    }

    void Update()
    {
        if (inUse)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

         //   Debug.Log( "camera mouse x: " + mouseX.ToString() + "camera mouse y: " + mouseY.ToString());
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -lookClampRange, lookClampRange);
            // cannot be above or below a certain range. 
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playertarget.transform.RotateAround(playertarget.transform.position, Vector3.up, mouseX);

        }
    }

    public void TurnOn()
    {
        inUse = true;
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor doesn't move.
    }
    public void TurnOff()
    {
        inUse = false;
        Cursor.lockState = CursorLockMode.None;
    }

}
