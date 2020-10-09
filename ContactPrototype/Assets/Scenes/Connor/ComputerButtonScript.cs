using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerButtonScript : MonoBehaviour
{
    public GameObject fromScreen;
    public GameObject toScreen;
    public Button thisButton;

    private void OnEnable()
    {
        thisButton.onClick.AddListener(() => DoButtonFunctions());
    }

    private void DoButtonFunctions()
    {
        ChangeMenus();
    }

    public void ChangeMenus()
    {
        fromScreen.SetActive(false);
        toScreen.SetActive(true);
    }

}
