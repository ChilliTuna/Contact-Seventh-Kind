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

    public bool isMovementDisabled = false;

    Rigidbody OurRigid;

    float hAxis;
    float vAxis;

    void Start()
    {
        OurRigid = GetComponent<Rigidbody>();
        currentSpeed = startSpeed;
    }

    void Update()
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
            else if (currentSpeed != startSpeed)
            {
                currentSpeed -= accerlationAmount * Time.deltaTime;
                if (currentSpeed < startSpeed)
                {
                    currentSpeed = startSpeed;
                }
            }
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
