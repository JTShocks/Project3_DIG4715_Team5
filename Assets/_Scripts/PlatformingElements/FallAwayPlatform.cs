using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAwayPlatform : MonoBehaviour
{

    //There will be an animator tied to this object that will play the proper animation
    //Temporary solution, the place of the Platform just disappears

    enum FallingPlatformState{
        Normal,
        Falling,
        Recovery
    }
    //Elements needed
    //Duration before dropping
    //Recovery time

    [SerializeField] GameObject platform;

    [SerializeField] float recoveryTime;

    [SerializeField] AudioSource audioSource;
    float currentRecoveryTime;
    float currentDelayTime;
    [SerializeField] float delayBeforeFalling;

    private FallingPlatformState state;

    void Awake()
    {
        state = FallingPlatformState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case FallingPlatformState.Normal:

            break;
            case FallingPlatformState.Falling:
            currentDelayTime -= Time.deltaTime;
            if(currentDelayTime <= 0)
            {

                if (audioSource != null)
                audioSource.Play();

                //Tell the animation to make the thing fall
                //Disable the collider for that time
                currentRecoveryTime = recoveryTime;
                platform.SetActive(false);
                state = FallingPlatformState.Recovery;
            }
            break;
            case FallingPlatformState.Recovery:
            currentRecoveryTime -= Time.deltaTime;
            if(currentRecoveryTime <= 0)
            {
                //Play the reset animation
                //Set the state back to normal
                platform.SetActive(true);
                state = FallingPlatformState.Normal;
            }
            break;
            default:
            break;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //This checks for the player standing ont the fallaway platform
        CharacterController character = other.GetComponent<CharacterController>();
        if(character != null && state == FallingPlatformState.Normal)
        {
            currentDelayTime = delayBeforeFalling;
            state = FallingPlatformState.Falling;
        }
    }
}
