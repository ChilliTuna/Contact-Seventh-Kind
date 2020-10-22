using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DynamicWorld : MonoBehaviour
{
    public GameObject WorldStateOne;
    public GameObject WorldStateTwo;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (WorldStateOne.active)
        {
            print("Two");
            WorldStateOne.SetActive(false);
            WorldStateTwo.SetActive(true);
        }
        else if (WorldStateTwo.active)
        {
            print("One");
            WorldStateTwo.SetActive(false);
            WorldStateOne.SetActive(true);
        }
    }


    public void TurnWorldStateOn(GameObject worldState)
    {
        if (!worldState.active)
        {
            worldState.SetActive(true);
        }
    
    }

    public void TurnWorldStateOff(GameObject worldState)
    {

        if (worldState.active)
        {
            worldState.SetActive(false);
        }
    }

    public void TurnWorldStateSwap(GameObject worldStateOn, GameObject worldStateOff)
    {

        if (!worldStateOn.active)
        {
            worldStateOn.SetActive(true);
        }


        if (worldStateOff.active)
        {
            worldStateOff.SetActive(false);
        }
    }














}
