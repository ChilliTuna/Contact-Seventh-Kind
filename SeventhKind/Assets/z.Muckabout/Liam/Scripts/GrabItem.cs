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



    public bool isGrabbing;
    public bool isInvestigating;

    public CameraToMouse CameraMouseTarget;
    public PlayerControls playerControlsTarget;

    public Collider itemBeingHeld;

    void Update()
    {
        Vector3 newLocation = new Vector3(0, 0, 0);

        //One press of right = turns on moving object with mouse 
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
                float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

                itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.up, -(mouseX * mouseSensitivity));
                 itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.left, -(mouseY * mouseSensitivity));

            //    itemBeingHeld.transform.localRotation = Quaternion.Euler(itemBeingHeld.transform.localRotation.x + (mouseX * mouseSensitivity), itemBeingHeld.transform.localRotation.y, itemBeingHeld.transform.localRotation.z);

                // itemBeingHeld.transform.eulerAngles = new Vector3(itemBeingHeld.transform.eulerAngles.x + (mouseY * mouseSensitivity), itemBeingHeld.transform.eulerAngles.y , itemBeingHeld.transform.eulerAngles.z);
                Debug.Log(mouseX.ToString());

            }
        }

        if (isGrabbing)
        {
          
          // x

              if (((itemBeingHeld.transform.position.x <= desiredLocation.transform.position.x + (pullSpeed * Time.deltaTime))
            && (itemBeingHeld.transform.position.x >= desiredLocation.transform.position.x - (pullSpeed * Time.deltaTime)))
            || itemBeingHeld.transform.position.x == desiredLocation.transform.position.x)
              {
                  // just round up 
                  newLocation.x = desiredLocation.transform.position.x;
              }
              else if (itemBeingHeld.transform.position.x > desiredLocation.transform.position.x)
              {
                  newLocation.x = itemBeingHeld.transform.position.x - pullSpeed * Time.deltaTime;
              }
              else if (itemBeingHeld.transform.position.x < desiredLocation.transform.position.x)
              {
                  newLocation.x = itemBeingHeld.transform.position.x + pullSpeed * Time.deltaTime;
              }

            if (((itemBeingHeld.transform.position.y <= desiredLocation.transform.position.y + (pullSpeed * Time.deltaTime))
     && (itemBeingHeld.transform.position.y >= desiredLocation.transform.position.y - (pullSpeed * Time.deltaTime)))
     || itemBeingHeld.transform.position.y == desiredLocation.transform.position.y)
            {
                // just round up 
                newLocation.y = desiredLocation.transform.position.y;
            }
            else if (itemBeingHeld.transform.position.y > desiredLocation.transform.position.y)
            {
                newLocation.y = itemBeingHeld.transform.position.y - pullSpeed * Time.deltaTime;
            }
            else if (itemBeingHeld.transform.position.y < desiredLocation.transform.position.y)
            {
                newLocation.y = itemBeingHeld.transform.position.y + pullSpeed * Time.deltaTime;
            }


            if (((itemBeingHeld.transform.position.z <= desiredLocation.transform.position.z + (pullSpeed * Time.deltaTime))
     && (itemBeingHeld.transform.position.z >= desiredLocation.transform.position.z - (pullSpeed * Time.deltaTime)))
     || itemBeingHeld.transform.position.z == desiredLocation.transform.position.z)
            {
                // just round up 
                newLocation.z = desiredLocation.transform.position.z;
            }
            else if (itemBeingHeld.transform.position.z > desiredLocation.transform.position.z)
            {
                newLocation.z = itemBeingHeld.transform.position.z - pullSpeed * Time.deltaTime;
            }
            else if (itemBeingHeld.transform.position.z < desiredLocation.transform.position.z)
            {
                newLocation.z = itemBeingHeld.transform.position.z+ pullSpeed * Time.deltaTime;
            }


            itemBeingHeld.transform.position = newLocation;
          }

          //  itemBeingHeld.transform.position = desiredLocation.transform.position;
        }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == tagToFind)
        {
            //One press of left click = pick up 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                itemBeingHeld = other;

                if (isGrabbing  && !isInvestigating)
                    {
                    //drop item 
                        isGrabbing = false;
                        other.GetComponent<Rigidbody>().useGravity = true;
                    playerControlsTarget.isMovementDisabled = false; 
                }
                    else
                    {
                    // item picked up
                        isGrabbing = true;
                        other.GetComponent<Rigidbody>().useGravity = false;
                    playerControlsTarget.isMovementDisabled = true; // player stops moving
                    Debug.Log("Grabbed");
                    }      
            }
        }
    }
}
