using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFixedPosition : MonoBehaviour
{

    public string tagToFind;
    public bool movementPathSet;
    public bool movementFinsihed;

    Vector3 desiredLocationCopy;
   Vector3 desiredPositionCopy;

    public GameObject playerMainBody;

    public float movementSpeed;

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
                }
            }

            float xMovementAmount = 0;
            float yMovementAmount = 0;
            float zMovementAmount = 0;


            //x 
            if ((playerMainBody.transform.position.x <= desiredLocationCopy.x + movementSpeed * Time.deltaTime)
                && (playerMainBody.transform.position.x >= desiredLocationCopy.x - movementSpeed * Time.deltaTime))
            {            
                    playerMainBody.transform.position = new Vector3(desiredLocationCopy.x, playerMainBody.transform.position.y, playerMainBody.transform.position.z);           
            }
            else if (playerMainBody.transform.position.x > desiredLocationCopy.x)
            {
                xMovementAmount -= movementSpeed * Time.deltaTime;
            }
            else if (playerMainBody.transform.position.x < desiredLocationCopy.x)
            {
                xMovementAmount += movementSpeed * Time.deltaTime;
            }

            //y 
            if ((playerMainBody.transform.position.y <= desiredLocationCopy.y + movementSpeed * Time.deltaTime)
                && (playerMainBody.transform.position.y >= desiredLocationCopy.y - movementSpeed * Time.deltaTime))
            {
                playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x, desiredLocationCopy.y, playerMainBody.transform.position.z);
            }
            else if (playerMainBody.transform.position.y > desiredLocationCopy.y)
            {
                yMovementAmount -= movementSpeed * Time.deltaTime;
            }
            else if (playerMainBody.transform.position.y < desiredLocationCopy.y)
            {
                yMovementAmount += movementSpeed * Time.deltaTime;
            }

            //z 
            if ((playerMainBody.transform.position.z <= desiredLocationCopy.z + movementSpeed * Time.deltaTime)
                && (playerMainBody.transform.position.z >= desiredLocationCopy.z - movementSpeed * Time.deltaTime))
            {
                playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x, playerMainBody.transform.position.y, desiredLocationCopy.z );
            }
            else if (playerMainBody.transform.position.z > desiredLocationCopy.z)
            {
                zMovementAmount -= movementSpeed * Time.deltaTime;
            }
            else if (playerMainBody.transform.position.z < desiredLocationCopy.x)
            {
                zMovementAmount += movementSpeed * Time.deltaTime;
            }

            // moves the player but how much needs to move them this frame 
            playerMainBody.transform.position = new Vector3(playerMainBody.transform.position.x + xMovementAmount,  playerMainBody.transform.position.y + yMovementAmount, playerMainBody.transform.position.z+ zMovementAmount);

            if ((playerMainBody.transform.position.x == desiredLocationCopy.x) && (playerMainBody.transform.position.y == desiredLocationCopy.y) && (playerMainBody.transform.position.z == desiredLocationCopy.z))
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
                desiredLocationCopy = other.GetComponent<FixedPositionSet>().desiredLocation;
            }
        }
    }

    void TurnOffFreeMovement()
    {

    }

}
