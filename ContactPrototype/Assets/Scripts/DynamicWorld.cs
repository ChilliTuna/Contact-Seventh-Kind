using System.Collections;
using System.Collections.Generic;
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
}
