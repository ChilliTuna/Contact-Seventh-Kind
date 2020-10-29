using UnityEngine;

public class ComputerWorldTrigger : MonoBehaviour
{
    private ComputerMainScript computer;
    public GameObject[] worldStates;
    private int currentWorldState = 0;

    private void Start()
    {
        computer = GetComponent<ComputerMainScript>();
    }

    public void ProgressWorldState()
    {
        if (!computer.activeUser.hasProgressed)
        {
            if (currentWorldState < worldStates.Length)
            {
                worldStates[currentWorldState].SetActive(!worldStates[currentWorldState].activeInHierarchy);
                currentWorldState++;
                computer.activeUser.hasProgressed = true;
            }
        }
    }
}