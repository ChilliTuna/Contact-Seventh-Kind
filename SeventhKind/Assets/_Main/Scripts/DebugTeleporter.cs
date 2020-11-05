using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleporter : MonoBehaviour
{
    public GameObject[] teleportLocations;

    private void Update()
    {
        for (int i = 0; i < teleportLocations.Length; i++)
        {
            if (Input.GetKey("" + (i + 1)))
            {
                gameObject.transform.position = teleportLocations[i].transform.position;
            }
        }
    }
}
