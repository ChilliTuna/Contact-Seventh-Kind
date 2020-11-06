using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Roomba : MonoBehaviour
{
    [Tooltip("If the roomba escapes the map, if it has nothing under it will teleport back the this point")]
    public Vector3 roombaRespawn;

    [Tooltip("0-Left, 1-Centre, 2-Right")]
    public GameObject[] roombaSensors;
    public GameObject followObject;
    public GameObject floorCheck;

    RaycastHit hitLeft;
    RaycastHit hitCentreLeft;
    RaycastHit hitCentre;
    RaycastHit hitCentreRight;
    RaycastHit hitRight;
    RaycastHit hitFloor;




    //1-left
    //2-right
    public int direction = 0;

    Rigidbody rb;

    public int interpolationFramesCount;

    Quaternion lerpTo;

    public float counter = 0;
    public bool avoidance = false;
    Vector3 currentEulerAngles;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        Physics.Linecast(transform.position, roombaSensors[0].transform.position, out hitLeft);
        UnityEngine.Debug.DrawLine(transform.position, roombaSensors[0].transform.position, Color.green);
        
        Physics.Linecast(transform.position, floorCheck.transform.position, out hitFloor);
        UnityEngine.Debug.DrawLine(transform.position, floorCheck.transform.position, Color.green);

        Physics.Linecast(transform.position, roombaSensors[1].transform.position, out hitCentreLeft);
        UnityEngine.Debug.DrawLine(transform.position, roombaSensors[1].transform.position, Color.green);

        Physics.Linecast(transform.position, roombaSensors[2].transform.position, out hitCentre);
        UnityEngine.Debug.DrawLine(transform.position, roombaSensors[2].transform.position, Color.green);

        Physics.Linecast(transform.position, roombaSensors[3].transform.position, out hitCentreRight);
        UnityEngine.Debug.DrawLine(transform.position, roombaSensors[3].transform.position, Color.green);

        Physics.Linecast(transform.position, roombaSensors[4].transform.position, out hitRight);
        UnityEngine.Debug.DrawLine(transform.position, roombaSensors[4].transform.position, Color.green);

        if (!hitFloor.collider)
        {
            transform.position = roombaRespawn;
        }



        //print(hitCentre.distance);
        if (gameObject.transform.position.y != 0.049f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.049f, gameObject.transform.position.z);
        }
        if (!avoidance)
        {
            if (direction == 0)
            {
                if (hitCentre.distance != 0)
                {
                    if (hitRight.distance == 0 || hitLeft.distance == 0)
                    {
                        if (hitRight.distance != 0)
                        {
                            print("1");
                            lerpTo.y -= 30;
                            direction = 1;
                        }
                        else
                        {
                            print("2");
                            lerpTo.y += 30;
                            direction = 1;
                        }
                    }
                    else
                    {
                        print("3");
                        lerpTo.y -= 180;
                        direction = 1;
                    }
                }
                else if (hitCentreLeft.distance != 0 || hitCentreRight.distance != 0)
                {
                    if (hitCentreLeft.distance != 0 && hitCentreRight.distance != 0)
                    {
                        lerpTo.y -= 50;
                        direction = 1;
                    }
                    if (hitCentreLeft.distance != 0)
                    {
                        lerpTo.y += 25;
                        direction = 1;
                        avoidance = true;
                    }
                    if (hitCentreRight.distance != 0)
                    {
                        lerpTo.y -= 25;
                        direction = 2;
                        avoidance = true;
                    }

                }


            }
            else if (direction == 1 || direction == 2)
            {
                if (lerpTo.y >= 360)
                {
                    lerpTo.y -= 360;
                }
                else if (lerpTo.y < 0)
                {
                    lerpTo.y -= 360;
                }

                if (currentEulerAngles.y > 360)
                {
                    currentEulerAngles.y -= 360;
                }
                else if(currentEulerAngles.y < 0)
                {
                    currentEulerAngles.y += 360;
                }

                if (lerpTo.y < 0 && lerpTo.y != gameObject.transform.localEulerAngles.y)
                {
                    //transform.Rotate(lerpTo * 1.5f * Time.deltaTime);
                    //  Vector3.Lerp(transform.rotation.eulerAngles, lerpTo, 15f);
                    //transform.rotation = new Quaternion(0, Mathf.Lerp(transform.rotation.eulerAngles.y, lerpTo.y, 100 * Time.deltaTime), 0, 1);
                    //gameObject.transform.rotation = 12;

                    if (gameObject.transform.rotation.y < lerpTo.y)
                    {
                        currentEulerAngles -= new Vector3(0, transform.rotation.eulerAngles.y - 5f, 0) * Time.deltaTime * 5;
                        transform.eulerAngles = currentEulerAngles;
                    }
                    else
                    {
                        currentEulerAngles += new Vector3(0, transform.rotation.eulerAngles.y - 5f, 0) * Time.deltaTime * 5;
                        transform.eulerAngles = currentEulerAngles;
                        //gameObject.transform.rotation.eulerAngles = new Vector3(0, transform.rotation.y - 3, 0);
                    }







                    print("5");
                }
                else
                {
                    if (gameObject.transform.rotation.y < lerpTo.y)
                    {
                        currentEulerAngles += new Vector3(0, transform.rotation.eulerAngles.y + 5f, 0) * Time.deltaTime * 5;
                        transform.eulerAngles = currentEulerAngles;
                    }
                    else
                    {

                        currentEulerAngles -= new Vector3(0, transform.rotation.eulerAngles.y - 5f, 0) * Time.deltaTime * 5;
                        transform.eulerAngles = currentEulerAngles;
                    }
                    print("4");
                    //transform.Rotate(lerpTo * 1.5f * Time.deltaTime);
                    // transform.rotation = Quaternion.Slerp(transform.rotation, lerpTo, 15);
                    // transform.rotation = new Quaternion(0, Mathf.Lerp(transform.rotation.eulerAngles.y, lerpTo.y, 100 * Time.deltaTime), 0, 1);



                }


                if (lerpTo.y - gameObject.transform.localEulerAngles.y < 1)
                {

                    direction = 0;
                }

            }



        }
        else
        {
            counter += Time.deltaTime;

            if (counter > 0.3f)
            {
                if (direction == 1)
                {
                    lerpTo.y += 55;
                }
                if (direction == 2)
                {
                    lerpTo.y -= 55;
                }
                avoidance = false;
               // counter = 0;
            }

        }
    }


    void FixedUpdate()
    {

        if (direction == 0)
        {
            print("MOOVING");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, followObject.transform.position, 0.033f);

            // print(hitCentre.distance);
        }


    }
}
