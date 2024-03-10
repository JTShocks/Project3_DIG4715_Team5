using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;
    // Set the duration of the fade.
    public float fadeDuration = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        // Set color of fade.
        fadeImage.color = Color.clear;
    }

    public void ActivateFadeIn()
    {
        // Use coroutine for delay.
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / fadeDuration);
            yield return null;
        }
    }

    public void ActivateFadeOut()
    {
        // Use coroutine for delay.
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / fadeDuration);
            yield return null;
        }
    }
}