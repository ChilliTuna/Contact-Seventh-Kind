using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    // Start is called before the first frame update

    public string tagToFind;
    public GameObject desiredLocation; // where you want the picked up item to be flung
    public float pullSpeed; // How fast the object moves towards the desired location

    public float mouseSensitivity;

  Vector3 newLocation;

    public bool isGrabbing;
    public bool isInvestigating;

    public CameraToMouse CameraMouseTarget;
    public PlayerControls playerControlsTarget;

    public float epic = 2;

    // Update is called once per frame
    void Update()
    {
    }

     void OnTriggerExit(Collider other)
    {
        //  If flinged off will drop to the ground
        if (other.tag == tagToFind)
        {
            isGrabbing = false;
            other.GetComponent<Rigidbody>().useGravity = true;
            playerControlsTarget.isMovementDisabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == tagToFind)
        {
            //One press of left click
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {      
                    if (isGrabbing  && !isInvestigating)
                    {
                        isGrabbing = false;
                        other.GetComponent<Rigidbody>().useGravity = true;

                        Debug.Log("Dropped");
                    }
                    else
                    {
                        isGrabbing = true;
                        other.GetComponent<Rigidbody>().useGravity = false;
                    Debug.Log("Grabbed");
                    }
          
            }

            //One press of right 
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (isGrabbing && !isInvestigating)
                {
                    isInvestigating = true;
                    playerControlsTarget.isMovementDisabled = true;
                    CameraMouseTarget.TurnOff();
                }
                else
                {
                    isInvestigating = false;
                    playerControlsTarget.isMovementDisabled = false;
                    CameraMouseTarget.TurnOn();
                }
            }


            //constant check of left click
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (isInvestigating)
                {
                    // moves 

                    float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
                    float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

                  //  other.transform.Rotate(mouseY * mouseSensitivity, -mouseX * mouseSensitivity, 0);
                  


                 other.transform.RotateAround(other.transform.position, Vector3.up, -(mouseX * mouseSensitivity) );
               other.transform.RotateAround(other.transform.position, Vector3.left, -(mouseY * mouseSensitivity));
                    Debug.Log(mouseX.ToString());

                }
            }

            if (isInvestigating)
            {

            }
            else if (isGrabbing)
            {
                // x
                if (other.transform.position.x != desiredLocation.transform.position.x)
                {
                    if (((other.transform.position.x <= desiredLocation.transform.position.x + (pullSpeed * Time.deltaTime))
                  && (other.transform.position.x >= desiredLocation.transform.position.x - (pullSpeed * Time.deltaTime)))
                  || other.transform.position.x == desiredLocation.transform.position.x)
                    {
                        // just round up 
                        newLocation.x = desiredLocation.transform.position.x;
                    }
                    else if (other.transform.position.x > desiredLocation.transform.position.x)
                    {
                        newLocation.x = other.transform.position.x - pullSpeed * Time.deltaTime;
                    }
                    else if (other.transform.position.x < desiredLocation.transform.position.x)
                    {
                        newLocation.x = other.transform.position.x + pullSpeed * Time.deltaTime;
                    }
                }
                // y 
                if (other.transform.position.y != desiredLocation.transform.position.y)
                {
                    if (((other.transform.position.y <= desiredLocation.transform.position.y + (pullSpeed * Time.deltaTime))
                  && (other.transform.position.y >= desiredLocation.transform.position.y - (pullSpeed * Time.deltaTime)))
                  || other.transform.position.y == desiredLocation.transform.position.y)
                    {
                        // just round up 
                        newLocation.y = desiredLocation.transform.position.y;
                    }
                    else if (other.transform.position.y > desiredLocation.transform.position.y)
                    {
                        newLocation.x = other.transform.position.y - pullSpeed * Time.deltaTime;
                    }
                    else if (other.transform.position.y < desiredLocation.transform.position.y)
                    {
                        newLocation.y = other.transform.position.y + pullSpeed * Time.deltaTime;
                    }
                }
                // z 
                if (other.transform.position.z != desiredLocation.transform.position.z)
                {
                    if (((other.transform.position.z <= desiredLocation.transform.position.z + (pullSpeed * Time.deltaTime))
                  && (other.transform.position.z >= desiredLocation.transform.position.z - (pullSpeed * Time.deltaTime)))
                  || other.transform.position.z == desiredLocation.transform.position.z)
                    {
                        // just round up 
                        newLocation.z = desiredLocation.transform.position.z;
                    }
                    else if (other.transform.position.z > desiredLocation.transform.position.z)
                    {
                        newLocation.z = other.transform.position.z - pullSpeed * Time.deltaTime;
                    }
                    else if (other.transform.position.z < desiredLocation.transform.position.z)
                    {
                        newLocation.z = other.transform.position.z + pullSpeed * Time.deltaTime;
                    }
                }

                other.transform.position = newLocation;
              //  Debug.Log("X: " + newLocation.x.ToString() + " Y: " + newLocation.y.ToString() + " Z: " + newLocation.z.ToString());
            }
        }
    }
}
