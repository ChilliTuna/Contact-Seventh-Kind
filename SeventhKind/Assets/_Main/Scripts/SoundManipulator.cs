using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManipulator : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAtRandomPitch(float range)
    {
        if (range > 3)
        {
            range = 3;
        }
        if (range < 0)
        {
            range = 0; 
        }
        audioSource.pitch = Random.Range(1 - (range / 3), 1 + (range / 1.5f));
        audioSource.Play();
    }
}
