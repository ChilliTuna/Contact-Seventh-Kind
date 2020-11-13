using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void MenuButton(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
    
    
  


    public void MenuExitButton()
    {
        Application.Quit();
    }
}

