using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audio.clip = clip;
        audio.Play();
        StartCoroutine(Audio());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Audio()
    {
        yield return new WaitUntil(() => audio.isPlaying == false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}
