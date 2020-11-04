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

    private string inputCode = "";
    private RaycastHit raycastHit;
    private bool hasPassed = false;
    private bool shouldFlashGreen = false;
    private bool shouldFlashRed = false;
    private float timePassedGreen;
    private float timePassedRed;
    private AudioSource audio;

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
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit))
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    if (Input.GetMouseButtonDown(0))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (raycastHit.collider.gameObject.name == "0" + i + "_ButtonCollider")
                            {
                                inputCode += i;
                                Debug.Log("Boop");
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