using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // An overload to allow for temporary audio to be played at certain positions.
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        GameObject tempAudioObject = new GameObject("TempAudio");
        tempAudioObject.transform.position = position;

        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.Play();

        Destroy(tempAudioObject, clip.length);
    }
}
