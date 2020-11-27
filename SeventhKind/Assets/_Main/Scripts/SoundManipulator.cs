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

    public void PlayAtRandomPitch()
    {
        audioSource.pitch = Random.Range(0.5f, 2);
        audioSource.Play();
    }
}
