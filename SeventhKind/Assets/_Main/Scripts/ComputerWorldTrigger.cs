using UnityEngine;

public class ComputerWorldTrigger : MonoBehaviour
{
    public int statesPerEvent;
    public GameObject[] worldStates;

    private ComputerMainScript computer;
    private int currentWorldState = 0;

    private void Start()
    {
        computer = GetComponent<ComputerMainScript>();
    }

    public void ProgressWorldState()
    {
        if (!computer.activeUser.progressesWorld)
        {
            for (int i = 0; i > statesPerEvent; i++)
            {
                if (currentWorldState < worldStates.Length)
                {
                    worldStates[currentWorldState].SetActive(!worldStates[currentWorldState].activeInHierarchy);
                    currentWorldState++;
                    computer.activeUser.progressesWorld = true;
                }
            }
        }
    }
}