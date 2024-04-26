using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public float stepRate = 0.5f;
    public CharacterController characterController;

    private float stepTimer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if(stepTimer >= stepRate)
            {
                stepTimer = 0f;
                PlayFootstepSound();
            }
        }
        else
        {
            stepTimer = 0;
        }
    }

    void PlayFootstepSound()
    {
        if(footstepSounds.Length > 0)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
