using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class PlayerControls : MonoBehaviour
{
    public float startSpeed;
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
 
        currentSpeed = startSpeed;
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
            else if (currentSpeed != startSpeed)
            {
                currentSpeed -= deaccerlationAmount * Time.deltaTime;
                if (currentSpeed < startSpeed)
                {
                    currentSpeed = startSpeed;
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
        if (!isMovementDisabled)
        {
            float forwardMovementAmount = vAxis * currentSpeed;
            float sideMovementAmount = hAxis * currentSpeed;

            OurRigid.position += forwardMovementAmount * transform.forward * (float)Time.deltaTime;
            OurRigid.position += sideMovementAmount * transform.right * (float)Time.deltaTime;

            //  this.transform.Translate(transform.forward * vAxis * Time.deltaTime);
            //   this.transform.Translate(transform.right * hAxis * Time.deltaTime);

            if ((forwardMovementAmount > maxSpeed) || (forwardMovementAmount < -maxSpeed)
                || (sideMovementAmount > maxSpeed) || (sideMovementAmount < -maxSpeed))
            {
                Debug.Log("UH OH!!!");
            }
            else
            {
               // Debug.Log("UP DOWN: " + forwardMovementAmount.ToString() + "  vAxis: " + vAxis.ToString());
               // Debug.Log("LEFT RIGHT: " + sideMovementAmount.ToString() + "  hAxis: " + hAxis.ToString());
            }
        }
    }

}
