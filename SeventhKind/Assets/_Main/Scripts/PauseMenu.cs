using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    PlayerControls controlScript;
    CameraToMouse CamToMouse;
    public GameObject pauseMenu;
    public Collider colliderInfrontOfPlayer;
    bool paused;
    InteractWithUIObject interactWithUIObject;

    void Start()
    {
        colliderInfrontOfPlayer.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player");
        controlScript = player.GetComponent<PlayerControls>();
        interactWithUIObject = player.GetComponent<InteractWithUIObject>();
        CamToMouse = player.GetComponentInChildren<CameraToMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && Input.GetKeyDown(KeyCode.Escape) && !controlScript.isMovementDisabled)
        {

            interactWithUIObject.enabled = false;
            paused = true;
            controlScript.enabled = false;
            CamToMouse.enabled = false;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (paused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeButton();

        }
    }

    public void ResumeButton()
    {
        interactWithUIObject.enabled = true;

        paused = false;
        controlScript.enabled = true;
        CamToMouse.enabled = true;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MenuButton(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }


    public void ExitToDesktopButton()
    {
        Application.Quit();
    }

}
