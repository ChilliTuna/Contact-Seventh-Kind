//using System.Diagnostics;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    // Start is called before the first frame update

    public string tagToFind;
    public GameObject desiredLocation; // where you want the picked up item to be flung
    public float pullSpeed; // How fast the object moves towards the desired location

    public float SideMouseSensitivity;
    public float UpDownMouseSensitivity;

    public bool isGrabbing;
    public bool isInvestigating;

    public CameraToMouse CameraMouseTarget;
    public PlayerControls playerControlsTarget;
    public GameObject playerBody;

    public Collider itemBeingHeld;


    public float holdingPlayerRotationY;
    public float YRotationAmount;
    public float ZRotationAmount;


    void Update()
    {
        UpdateDifferencXYRotationAmounts();
        Vector3 newLocation = new Vector3(0, 0, 0);

        //One press of right = turns on investigating
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

                UpdateDifferencXYRotationAmounts();

              
                // normal 
                 itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.up, -(mouseX * SideMouseSensitivity));
                
                //y
                itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.left, -(mouseY * YRotationAmount * UpDownMouseSensitivity));
                //z
                itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.forward, (mouseY * ZRotationAmount * UpDownMouseSensitivity));

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

    void UpdateDifferencXYRotationAmounts()
    {
        // 0 to 360 

        holdingPlayerRotationY = playerBody.transform.eulerAngles.y;

        YRotationAmount = 0;

        //x
        if (90 > holdingPlayerRotationY &&  holdingPlayerRotationY > 0)
        {
            //A 0=1
            YRotationAmount = (90 - holdingPlayerRotationY) / 90;
        }
        else if (90 < holdingPlayerRotationY &&  holdingPlayerRotationY <180)
        {
            // B 180 =-1
            YRotationAmount = (90 - holdingPlayerRotationY) / 90;
        }
        else if (180 < holdingPlayerRotationY && holdingPlayerRotationY < 270)
        {
            // C 180 =-1
            YRotationAmount = (-270 + holdingPlayerRotationY) / 90;
        }
        else if (270 < holdingPlayerRotationY && holdingPlayerRotationY < 360)
        {
            // D 0 =1
          YRotationAmount = (-270 + holdingPlayerRotationY) / 90;
        }

        ZRotationAmount = 0;

        //y 
        if (90 > holdingPlayerRotationY && holdingPlayerRotationY > 0)
        {
            //A 90 = -1
            ZRotationAmount = (0 - holdingPlayerRotationY) / 90;
        }
        else if (90 < holdingPlayerRotationY && holdingPlayerRotationY < 180)
        {
            // B 90 =-1
           ZRotationAmount = (-180 + holdingPlayerRotationY) / 90;
        }
        else if (180 < holdingPlayerRotationY && holdingPlayerRotationY < 270)
        {
            // C 270 = 1
            ZRotationAmount = (-180 + holdingPlayerRotationY) / 90;
        }
        else if (270 < holdingPlayerRotationY && holdingPlayerRotationY < 360)
        {
            // D 270 = 1
            ZRotationAmount = (360 - holdingPlayerRotationY) / 90;
        }
    }
}
