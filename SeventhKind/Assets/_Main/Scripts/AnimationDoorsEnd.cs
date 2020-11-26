using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDoorsEnd : MonoBehaviour
{


    public Animator doorAnim;
    public AudioClip Normal;
    public AudioClip MedBay;
    AudioSource audio;
    bool hasRun = false;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Normal;
        audio.loop = true;
        audio.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasRun)
        {
            hasRun = true;
            audio.loop = false;
            audio.volume = 0.2f;
            doorAnim.SetBool("IsOpening", true);
            audio.clip = MedBay;
            audio.Play();

        }
    }
}

