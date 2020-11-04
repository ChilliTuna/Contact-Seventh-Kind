using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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

            float xMovementAmount = 0;
            float yMovementAmount = 0;
            float zMovementAmount = 0;

            float xRotationAmount = 0;
            float yRotationAmount = 0;
            float zRotationAmount = 0;

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


            //rotation
            //x 
            if ((playerMainBody.transform.rotation.x <= desiredRotationCopy.x + rotationSpeed * Time.deltaTime)
                && (playerMainBody.transform.rotation.x >= desiredRotationCopy.x - rotationSpeed * Time.deltaTime))
            {
                playerMainBody.transform.rotation = new Quaternion(desiredRotationCopy.x, playerMainBody.transform.rotation.y, playerMainBody.transform.rotation.z, 1);
                    }
            else if (playerMainBody.transform.rotation.x > desiredRotationCopy.x)
            {
               xRotationAmount -= rotationSpeed * Time.deltaTime;
            }

            //y 
            if ((playerMainBody.transform.rotation.y <= desiredRotationCopy.y + rotationSpeed * Time.deltaTime)
                  && (playerMainBody.transform.rotation.y >= desiredRotationCopy.y - rotationSpeed * Time.deltaTime))
            {
                playerMainBody.transform.rotation = new Quaternion(playerMainBody.transform.rotation.x, desiredRotationCopy.y, playerMainBody.transform.rotation.z, 1);
            }
            else if (playerMainBody.transform.rotation.y > desiredRotationCopy.y)
            {
                yRotationAmount -= rotationSpeed * Time.deltaTime;
            }

            //z 
            if ((playerMainBody.transform.rotation.z <= desiredRotationCopy.z + rotationSpeed * Time.deltaTime)
                 && (playerMainBody.transform.rotation.z >= desiredRotationCopy.z - rotationSpeed * Time.deltaTime))
            {
                playerMainBody.transform.rotation = new Quaternion(playerMainBody.transform.rotation.x, playerMainBody.transform.rotation.y, desiredRotationCopy.z, 1);
            }
            else if (playerMainBody.transform.rotation.z > desiredRotationCopy.z)
            {
                zRotationAmount -= rotationSpeed * Time.deltaTime;
            }


            // moves the player but how much needs to move them this frame 
            playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x + xMovementAmount,  playerMainBody.transform.position.y + yMovementAmount, playerMainBody.transform.position.z+ zMovementAmount);

            playerMainBody.transform.rotation = new Quaternion(playerMainBody.transform.rotation.x + xRotationAmount, playerMainBody.transform.rotation.y + yRotationAmount, playerMainBody.transform.rotation.z + zRotationAmount, 1);

            if ((playerMainBody.transform.position.x == desiredPositionCopy.x) && (playerMainBody.transform.position.y == desiredPositionCopy.y) && (playerMainBody.transform.position.z == desiredPositionCopy.z)  
                && (playerMainBody.transform.rotation.x == desiredRotationCopy.x) && (playerMainBody.transform.rotation.y == desiredRotationCopy.y) && (playerMainBody.transform.rotation.z == desiredRotationCopy.z))
            {
                movementFinsihed = true;
                
            }

            Debug.Log("x: " + xRotationAmount + "y: " + yRotationAmount + "z: " + zRotationAmount);
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
                TurnOffFreeMovement();
            }
        }
    }

    void TurnOffFreeMovement()
    {
        CameraTarget.TurnOff();
    }

    void TurnOnFreeMovement()
    {
        CameraTarget.TurnOn();
    }


}
