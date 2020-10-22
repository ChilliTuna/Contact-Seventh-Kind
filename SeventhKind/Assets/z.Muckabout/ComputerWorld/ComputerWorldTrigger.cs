using UnityEngine;
using UnityEngine.UI;

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
            worldStates[currentWorldState].SetActive(true);
            currentWorldState++;
            computer.activeUser.hasProgressed = true;
        }
    }
}