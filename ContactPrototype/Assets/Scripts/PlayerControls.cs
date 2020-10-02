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

    void FixedUpdate()
    {
        if (hAxis < 0)
        {
            hAxis *= (currentSpeed - backMovementPenality);
        }
        else
        {
            hAxis *= currentSpeed;
        }
        vAxis *= currentSpeed;

      OurRigid.position += vAxis * transform.forward * (float)Time.deltaTime;
      OurRigid.position += hAxis * transform.right * (float)Time.deltaTime;
    }
}
