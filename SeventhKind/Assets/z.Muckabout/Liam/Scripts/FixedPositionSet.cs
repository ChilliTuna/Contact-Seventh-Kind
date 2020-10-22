using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPositionSet : MonoBehaviour
{
    // A class used for forcing the player into a position and rotation. 
    // add this to an object that the player's "arm" will touch and click such as a box around the computer
    // GrabFixedPosition is a script which interacts with this script 

    // position and rotation is where you would like the player to be. 
    public Vector3 desiredLocation;
    public Vector3 deisredRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
