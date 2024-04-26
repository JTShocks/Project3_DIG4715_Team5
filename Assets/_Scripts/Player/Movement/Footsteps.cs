using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;


    public void PlayFootstepSound()
    {
        if(footstepSounds.Length > 0)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
