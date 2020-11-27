using System;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    public uint code;
    public Camera mainCamera;
    public float flashTime;
    public GameObject passLight;
    public GameObject failLight;
    public AudioClip passSound;
    public AudioClip failSound;

    public UnityEvent OnSuccess;
    public UnityEvent OnButtonPress;

    private string inputCode = "";
    private RaycastHit raycastHit;
    private bool hasPassed = false;
    private bool shouldFlashGreen = false;
    private bool shouldFlashRed = false;
    private float timePassedGreen;
    private float timePassedRed;
    private AudioSource audio;
    private bool doClick;

    private void Start()
    {
        audio = gameObject.transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!hasPassed)
        {
            if (mainCamera.transform.parent.GetComponent<InteractWithUIObject>().currentInteraction == gameObject)
            {
                if (gameObject.GetComponent<BoxCollider>().enabled)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    doClick = true;
                }
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit) && doClick)
                {
                    doClick = false;
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (raycastHit.collider.gameObject.name == "0" + i + "_ButtonCollider")
                            {
                                Debug.Log("Boop" + i);
                                OnButtonPress.Invoke();
                                inputCode += i;
                                if (Convert.ToInt32(inputCode) == code)
                                {
                                    Succeed();
                                }
                                else if (inputCode.Length > 3)
                                {
                                    Fail();
                                }
                            }
                            else if (raycastHit.collider.gameObject.name == "Enter_ButtonCollider")
                            {
                                inputCode = "";
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (shouldFlashRed)
        {
            failLight.SetActive(true);
            timePassedRed += 0.02f;
            if (timePassedRed >= flashTime)
            {
                failLight.SetActive(false);
                shouldFlashRed = false;
                timePassedRed = 0f;
            }
        }
        if (shouldFlashGreen)
        {
            passLight.SetActive(true);
            timePassedGreen += 0.02f;
            if (timePassedGreen >= flashTime)
            {
                passLight.SetActive(false);
                shouldFlashGreen = false;
                timePassedGreen = 0f;
            }
        }
    }

    private void Succeed()
    {
        hasPassed = true;
        shouldFlashGreen = true;
        audio.clip = passSound;
        audio.Play();
        OnSuccess.Invoke();
        gameObject.GetComponent<Interactable>().currentPlayer.GetComponent<InteractWithUIObject>().ExitInteraction();
        Debug.Log("Success");
    }

    private void Fail()
    {
        inputCode = "";
        shouldFlashRed = true;
        audio.clip = failSound;
        audio.Play();
        Debug.Log("Failure");
    }
}