using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class PlayerControls : MonoBehaviour
{
    public float minSpeed;
    public float currentSpeed;
    public float maxSpeed;
    public float backMovementPenality;
    public float accerlationAmount;
    public float deaccerlationAmount;

    public bool isMovementDisabled = false;

    public Rigidbody OurRigid;
    public CameraToMouse camerTarget;

    float hAxis;
    float vAxis;

    public bool isLockedIntoFinalScene;
    public UnityEngine.Vector3 finalSceneLocation;

    public float finalSceneMovement;

    void Start()
    {

        currentSpeed = minSpeed;
    }

    void Update()
    {
        hAxis = 0;
        vAxis = 0;

        if (isLockedIntoFinalScene)
        {
            FinalSceneMovement();
        }
        else
        {

        if (!isMovementDisabled)
        {
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");

            if (hAxis != 0 || vAxis != 0)
            {
                // button presses
                currentSpeed += accerlationAmount * Time.deltaTime;
                if (currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                }
            }
            else if (currentSpeed != minSpeed)
            {
                // Not moving = slow down
                currentSpeed -= deaccerlationAmount * Time.deltaTime;
                if (currentSpeed < minSpeed)
                {
                    currentSpeed = minSpeed;
                }
            }
        }

        // If the player isn't moving so we don't need to change the Y position. The slopes were pulling the player down. 
        if (hAxis == 0 && vAxis == 0)
        {
            OurRigid.useGravity = false;
            OurRigid.velocity = new UnityEngine.Vector3();
        }
        else
        {
            OurRigid.useGravity = true;
        }

        }
    }


    void FinalSceneMovement()
    {
        float xValue = 0;
        float yValue = 0;
        float zValue = 0;
        OurRigid.useGravity = false;

        camerTarget.TurnOffUse();

        //x
        if ((this.transform.position.x + finalSceneMovement * Time.deltaTime > finalSceneLocation.x)
            && (this.transform.position.x - finalSceneMovement * Time.deltaTime < finalSceneLocation.x))
        {
            this.transform.position = new UnityEngine.Vector3(finalSceneLocation.x, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < finalSceneLocation.x)
        {
            xValue = finalSceneMovement;
        }
        else
        {
            xValue = -finalSceneMovement;
        }

        //y
        if ((this.transform.position.y + finalSceneMovement * Time.deltaTime > finalSceneLocation.y) 
            && (this.transform.position.y - finalSceneMovement * Time.deltaTime < finalSceneLocation.y))
        {
            this.transform.position = new UnityEngine.Vector3(this.transform.position.x, finalSceneLocation.y, this.transform.position.z);
        }
        else if (this.transform.position.y < finalSceneLocation.y)
        {
            yValue = finalSceneMovement;
        }
        else
        {
            yValue = -finalSceneMovement;
        }

        //z
        if ((this.transform.position.z + finalSceneMovement * Time.deltaTime > finalSceneLocation.z)
            && (this.transform.position.z - finalSceneMovement * Time.deltaTime < finalSceneLocation.z))
        {
            this.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y, finalSceneLocation.z);
        }
        else if (this.transform.position.z < finalSceneLocation.z)
        {
            zValue = finalSceneMovement;
        }
        else
        {
            zValue = -finalSceneMovement;
        }

        //actually move

        UnityEngine.Vector3 amountToMove = new UnityEngine.Vector3(xValue,yValue,zValue).normalized;

        this.transform.position = new UnityEngine.Vector3(this.transform.position.x + (xValue * finalSceneMovement *Time.deltaTime), this.transform.position.y + (yValue * finalSceneMovement * Time.deltaTime), this.transform.position.z + (zValue * finalSceneMovement * Time.deltaTime));


      // OurRigid.position += amountToMove.x * transform.forward * finalSceneMovement * Time.deltaTime;
      //  OurRigid.position += amountToMove.y * transform.right * finalSceneMovement * Time.deltaTime;
        //OurRigid.position += amountToMove.y * transform.up * finalSceneMovement * Time.deltaTime;
    }


    void FixedUpdate()
    {
        // actually move

        UnityEngine.Vector3 amountToMove = new UnityEngine.Vector3(vAxis, hAxis, 0).normalized;
        OurRigid.position += amountToMove.x * transform.forward * currentSpeed * Time.deltaTime;
        OurRigid.position += amountToMove.y * transform.right * currentSpeed * Time.deltaTime;

     //   Debug.Log(amountToMove.x.ToString() + "  " + amountToMove.y.ToString());

    }
}