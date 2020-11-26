using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeBlack : MonoBehaviour
{
    public Image blackoutImage;
    private bool shouldFade;
    private bool fadeToBlack;

    public void FadeToBlack(float fadeSpeed)
    {
        shouldFade = true;
        fadeToBlack = true;
        StartCoroutine(DoFade(fadeSpeed));
    }

    public void FadeFromBlack(float fadeSpeed)
    {
        shouldFade = true;
        fadeToBlack = false;
        StartCoroutine(DoFade(fadeSpeed));
    }

    public void FadeOpposite(float fadeSpeed)
    {
        shouldFade = true;
        fadeToBlack = !fadeToBlack;
        StartCoroutine(DoFade(fadeSpeed));
    }

    private IEnumerator DoFade(float fadeSpeed = 5)
    {
        Color transparency = blackoutImage.GetComponent<Image>().color;

        if (shouldFade)
        {
            if (fadeToBlack)
            {
                while (transparency.a < 1)
                {
                    transparency.a += fadeSpeed * Time.deltaTime;
                    blackoutImage.GetComponent<Image>().color = transparency;
                    yield return null;
                }
                shouldFade = false;
            }
            else
            {
                while (transparency.a > 0)
                {
                    transparency.a -= fadeSpeed * Time.deltaTime;
                    blackoutImage.GetComponent<Image>().color = transparency;
                    yield return null;
                }
                shouldFade = false;
            }
        }

        yield break;
    }
}