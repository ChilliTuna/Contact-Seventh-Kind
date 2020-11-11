using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class GrabFixedPosition : MonoBehaviour
{
    
    public string tagToFind;
    public bool movementPathSet;
    public bool movementFinsihed;

    Vector3 desiredPositionCopy;
   public Vector3 desiredRotationCopy;

    public GameObject playerMainBody;

    public float movementSpeed;
    public float rotationSpeed;

    public CameraToMouse CameraTarget;

    float xMovementAmount = 0;
    float yMovementAmount = 0;
    float zMovementAmount = 0;



    // Update is called once per frame
    void Update()
    {

        if (movementPathSet)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (movementFinsihed)
                {
                   movementPathSet = false;
                    TurnOnFreeMovement();
                }
            }

          xMovementAmount = 0;
             yMovementAmount = 0;
           zMovementAmount = 0;

            //x 
            if ((playerMainBody.transform.position.x <= desiredPositionCopy.x + movementSpeed * Time.deltaTime)
                && (playerMainBody.transform.position.x >= desiredPositionCopy.x - movementSpeed * Time.deltaTime))
            {
                playerMainBody.transform.position = new Vector3(desiredPositionCopy.x, playerMainBody.transform.position.y, playerMainBody.transform.position.z);
            }
            else if (playerMainBody.transform.position.x > desiredPositionCopy.x)
            {
                xMovementAmount -= movementSpeed * Time.deltaTime;
            }
            else if (playerMainBody.transform.position.x < desiredPositionCopy.x)
            {
                xMovementAmount += movementSpeed * Time.deltaTime;
            }
           

            //y 
            if ((playerMainBody.transform.position.y <= desiredPositionCopy.y + movementSpeed * Time.deltaTime)
                && (playerMainBody.transform.position.y >= desiredPositionCopy.y - movementSpeed * Time.deltaTime))
            {
                playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x, desiredPositionCopy.y, playerMainBody.transform.position.z);
            }
            else if (playerMainBody.transform.position.y > desiredPositionCopy.y)
            {
                yMovementAmount -= movementSpeed * Time.deltaTime;
            }
            else if (playerMainBody.transform.position.y < desiredPositionCopy.y)
            {
                yMovementAmount += movementSpeed * Time.deltaTime;
            }

            //z 
            if ((playerMainBody.transform.position.z <= desiredPositionCopy.z + movementSpeed * Time.deltaTime)
                && (playerMainBody.transform.position.z >= desiredPositionCopy.z - movementSpeed * Time.deltaTime))
            {
                playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x, playerMainBody.transform.position.y, desiredPositionCopy.z );
            }
            else if (playerMainBody.transform.position.z > desiredPositionCopy.z)
            {
                zMovementAmount -= movementSpeed * Time.deltaTime;
            }
            else if (playerMainBody.transform.position.z < desiredPositionCopy.x)
            {
                zMovementAmount += movementSpeed * Time.deltaTime;
            }

            // moves the player but how much needs to move them this frame 
            playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x + xMovementAmount,  playerMainBody.transform.position.y + yMovementAmount, playerMainBody.transform.position.z+ zMovementAmount);


            if ((playerMainBody.transform.position.x == desiredPositionCopy.x) && (playerMainBody.transform.position.y == desiredPositionCopy.y) && (playerMainBody.transform.position.z == desiredPositionCopy.z))
            {
                movementFinsihed = true;          
            }     
        }

    }

    void OnTriggerStay(Collider other)
    {
        // if its the right tage
        if (other.tag == tagToFind)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //
                movementPathSet = true;
                desiredPositionCopy = other.GetComponent<FixedPositionSet>().desiredPosition;
                desiredRotationCopy = other.GetComponent<FixedPositionSet>().desiredRotation;
                playerMainBody.transform.rotation = Quaternion.Euler(desiredRotationCopy.x, desiredRotationCopy.y, desiredRotationCopy.z);
                // forces player into the right rotation
                TurnOffFreeMovement();
            }
        }
    }

    void TurnOffFreeMovement()
    {
        CameraTarget.TurnOffUse();
    }

    void TurnOnFreeMovement()
    {
        CameraTarget.TurnOnUse();
    }


}
