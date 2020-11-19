//using System.Diagnostics;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    // Start is called before the first frame update

    public string tagToFind;
    public GameObject desiredLocation; // where you want the picked up item to be flung
    public float pullSpeed; // How fast the object moves towards the desired location

    public float SideMouseSensitivity; // x rotation when investigating
    public float UpDownMouseSensitivity; // y and z rotation when investigating

    public float maxScrollDesiredLocationOffset;
    public float minScrollDesiredLocationOffset;

    public float currentScrollDesiredLocationOffset;
    public float scrollAmount;

    [HideInInspector]
    public bool isGrabbing;
    [HideInInspector]
    public bool isInvestigating;

    public CameraToMouse CameraMouseTarget;
    public PlayerControls playerControlsTarget;
    public GameObject playerBody;

    public Vector3 defaultdesiredLocation;

    private bool shouldInteract;
    private Collider itemBeingHeld;
    private float holdingPlayerRotationY;
    private float YRotationAmount;
    private float ZRotationAmount;
    private InteractWithUIObject interactScript;

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
                CameraMouseTarget.TurnOffUse();
                FreezeHoldingObject();
            }
            else
            {
                isInvestigating = false;
                UnFreezeHoldingObject();
                CameraMouseTarget.TurnOnUse();
            }
        }

        //constant check of left click = actually investiagte
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (isInvestigating)
            {
                float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

                UpdateDifferencXYRotationAmounts();

                // normal x
                itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.up, -(mouseX * SideMouseSensitivity));

                //y
                itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.left, -(mouseY * YRotationAmount * UpDownMouseSensitivity));
                //z
                itemBeingHeld.transform.RotateAround(itemBeingHeld.transform.position, Vector3.forward, (mouseY * ZRotationAmount * UpDownMouseSensitivity));
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
        {
            shouldInteract = true;
            Debug.Log("Button Pressed");
        }
        else
        {
            shouldInteract = false;
        }

  

        if (isGrabbing)
        {

            // scrolling
            float scrollDifference = Input.GetAxis("Mouse ScrollWheel");
            if (scrollDifference > 0f)
            {
                currentScrollDesiredLocationOffset += scrollAmount * Time.deltaTime;
                if (currentScrollDesiredLocationOffset > maxScrollDesiredLocationOffset)
                {
                    currentScrollDesiredLocationOffset = maxScrollDesiredLocationOffset;
                }
            }
            else if (scrollDifference < 0f)
            {
                currentScrollDesiredLocationOffset -= scrollAmount * Time.deltaTime;
                if (currentScrollDesiredLocationOffset < minScrollDesiredLocationOffset)
                {
                    currentScrollDesiredLocationOffset = minScrollDesiredLocationOffset;
                }
            }

            setDesiredLocation(new Vector3(0, 0, 2.15f + currentScrollDesiredLocationOffset));


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

            //y 
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

            
            //z
            if (((itemBeingHeld.transform.position.z  <= desiredLocation.transform.position.z + (pullSpeed * Time.deltaTime))
         && (itemBeingHeld.transform.position.z  >= desiredLocation.transform.position.z - (pullSpeed * Time.deltaTime)))
         || itemBeingHeld.transform.position.z  == desiredLocation.transform.position.z)
            {
                // just round up
                newLocation.z = desiredLocation.transform.position.z;
            }
            else if (itemBeingHeld.transform.position.z > desiredLocation.transform.position.z )
            {
                newLocation.z = itemBeingHeld.transform.position.z - pullSpeed * Time.deltaTime;
            }
            else if (itemBeingHeld.transform.position.z < desiredLocation.transform.position.z )
            {
                newLocation.z = itemBeingHeld.transform.position.z + pullSpeed * Time.deltaTime;
            }

            itemBeingHeld.transform.position = newLocation;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (shouldInteract)
        {
            if (other.tag == tagToFind)
            {
                itemBeingHeld = other;

                if (isGrabbing && !isInvestigating)
                {
                    //drop item 
                    isGrabbing = false;
                    other.GetComponent<Rigidbody>().useGravity = true; ;
                    playerControlsTarget.isMovementDisabled = false;
                    currentScrollDesiredLocationOffset = 0;
                    playerControlsTarget.isMovementDisabled = false;
                }
                else
                {
                    // item picked up
                    isGrabbing = true;
                    other.GetComponent<Rigidbody>().useGravity = false;
                    playerControlsTarget.isMovementDisabled = true; // player stops moving  
                    try
                    {
                        Vector3 holdingDesiredLocation = other.GetComponent<ItemsDistanceFromPlayer>().desiredDisatnceOffsetFromPlayer;

                        setDesiredLocation(holdingDesiredLocation);
                    }
                    catch
                    {
                        setDesiredLocation(defaultdesiredLocation);
                    }
                    try
                    {
                        itemBeingHeld.GetComponent<ItemOutput>().Interact();
                    }
                    catch {}

                }
            }
        }
    }

    private void UpdateDifferencXYRotationAmounts()
    {
        // 0 to 360 
        holdingPlayerRotationY = playerBody.transform.eulerAngles.y;
        YRotationAmount = 0;

        //x
        if (90 > holdingPlayerRotationY && holdingPlayerRotationY > 0)
        {
            //A 0=1
            YRotationAmount = (90 - holdingPlayerRotationY) / 90;
        }
        else if (90 < holdingPlayerRotationY && holdingPlayerRotationY < 180)
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

    private void FreezeHoldingObject()
    {

        itemBeingHeld.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void UnFreezeHoldingObject()
    {

        itemBeingHeld.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


    }

    private void setDesiredLocation(Vector3 newDesiredLocation)
    {
        //desiredLocation.transform.position = new Vector3(x, y, z);
       desiredLocation.transform.localPosition = new Vector3(newDesiredLocation.x, newDesiredLocation.y, newDesiredLocation.z);
    }
}