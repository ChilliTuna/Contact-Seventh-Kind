using UnityEngine;

public class ComputerWorldTrigger : MonoBehaviour
{
    public GameObject gameMaster;

    private ComputerMainScript computer;

    private void Start()
    {
        computer = GetComponent<ComputerMainScript>();
    }

    public void ComputerProgressWorldState()
    {
        if (computer.activeUser.progressesWorld)
        {
            gameMaster.GetComponent<WorldStateManager>().ProgressWorldState();
            computer.activeUser.progressesWorld = false;
        }
    }
}