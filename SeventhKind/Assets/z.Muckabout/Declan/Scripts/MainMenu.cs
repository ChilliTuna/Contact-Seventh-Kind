using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MenuButton(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
    
    
  


    public void MenuExitButton(int SceneNumber)
    {
        Application.Quit();
    }
}

