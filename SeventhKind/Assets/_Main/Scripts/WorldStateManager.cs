using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    public int statesPerEvent;
    public GameObject[] worldStates;

    [HideInInspector]
    public int currentWorldState = 0;

    public void ProgressWorldState()
    {
        for (int i = 0; i < statesPerEvent; i++)
        {
            if (currentWorldState < worldStates.Length)
            {
                if (worldStates[currentWorldState] != null)
                {
                    worldStates[currentWorldState].SetActive(!worldStates[currentWorldState].activeInHierarchy);
                }
                currentWorldState++;
            }
        }
    }
}