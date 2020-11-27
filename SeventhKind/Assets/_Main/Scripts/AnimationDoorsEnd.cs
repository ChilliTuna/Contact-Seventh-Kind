using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnimationDoorsEnd : MonoBehaviour
{


    public Animator doorAnim;
    public AudioClip Normal;
    public AudioClip MedBay;
    public AudioClip Ending;
    AudioSource audio;
    bool hasRun = false;

    GameObject player;
    PlayerControls controls;

    public float TimeBeforeEventHappens;
    public float TimeBeforeDoorsOpen;
    public float TimeBeforePlayerStartsWalking;
    public float TimeBeforeFadeOut;

    public FadeBlack fadeOut;
    public CameraToMouse cam2Mouse;

    void Start()
    {
        fadeOut.FadeFromBlack(0.2f);
        player = GameObject.FindGameObjectWithTag("Player");
        controls = player.GetComponent<PlayerControls>();
        audio = GetComponent<AudioSource>();
        audio.clip = Normal;
        audio.loop = true;
        audio.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasRun)
        {
            if (other.tag == "Player")
            {
            StartCoroutine(CountV2(TimeBeforeEventHappens));
            controls.isMovementDisabled = true;
            cam2Mouse.enabled = false;
            }


        }
    }


    IEnumerator CountTo(float timing, bool iswalking)
    {
        yield return new WaitForSeconds(timing);
        if (!iswalking)
        {

            audio.clip = MedBay;
            audio.Play();
        }
        else
        {
            controls.isLockedIntoFinalScene = true;
        }
        
    }

    IEnumerator Count(float timing)
    {
        yield return new WaitForSeconds(timing);
        fadeOut.FadeToBlack(0.2f);
        yield return new WaitUntil(() => fadeOut.shouldFade == false);
            audio.clip = Ending;
            audio.Play();
            yield return new WaitUntil(() => audio.isPlaying == false);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        
    }

    IEnumerator CountV2(float timing)
    {
        yield return new WaitForSeconds(timing);
        Cursor.visible = false;

        hasRun = true;
        audio.loop = false;
        audio.volume = 0.2f;
        doorAnim.SetBool("IsOpening", true);
        StartCoroutine(CountTo(TimeBeforeDoorsOpen, false));

        StartCoroutine(CountTo(TimeBeforePlayerStartsWalking, true));
        StartCoroutine(Count(TimeBeforeFadeOut));
    }


}

