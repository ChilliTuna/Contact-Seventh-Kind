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

    float hAxis;
    float vAxis;

    void Start()
    {

        currentSpeed = minSpeed;
    }

    void Update()
    {
        hAxis = 0;
        vAxis = 0;

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

    void FixedUpdate()
    {
        // actually move

        UnityEngine.Vector3 amountToMove = new UnityEngine.Vector3(vAxis, hAxis, 0).normalized;
        OurRigid.position += amountToMove.x * transform.forward * currentSpeed * Time.deltaTime;
        OurRigid.position += amountToMove.y * transform.right * currentSpeed * Time.deltaTime;

     //   Debug.Log(amountToMove.x.ToString() + "  " + amountToMove.y.ToString());

    }
}