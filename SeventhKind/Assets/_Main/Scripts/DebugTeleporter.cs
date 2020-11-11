using UnityEngine;

public class DebugTeleporter : MonoBehaviour
{
    public GameObject[] teleportLocations;

    private void Update()
    {
        if (!gameObject.GetComponent<InteractWithUIObject>().isInteracting)
        {
            for (int i = 0; i < teleportLocations.Length; i++)
            {
                if (Input.GetKey("" + (i + 1)) && Input.GetKey(KeyCode.LeftAlt))
                {
                    gameObject.transform.position = teleportLocations[i].transform.position;
                }
            }
        }
    }
}