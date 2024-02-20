using UnityEngine;
using System.Collections;

public class AudioFade : MonoBehaviour
{

    public AudioSource audioSource;

    public float fadeInTime;
    public float fadeOutTime;
    public float delayBeforeFadeOut;

    void Start()
    {
        StartCoroutine(FadeIn(fadeInTime));
        Invoke("StartFadeOut", delayBeforeFadeOut);
    }

    public IEnumerator FadeIn(float time)
    {
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut(fadeOutTime));
    }

    public IEnumerator FadeOut(float time)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / time;
            yield return null;
        }

        audioSource.Stop();
    }
}
