using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAwayPlatform : MonoBehaviour
{

    enum FallingPlatformState{
        Normal,
        Falling,
        Recovery
    }
    //Elements needed
    //Duration before dropping
    //Recovery time

    [SerializeField] float recoveryTime;
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
                //Tell the animation to make the thing fall
                //Disable the collider for that time
                currentRecoveryTime = recoveryTime;
                state = FallingPlatformState.Recovery;
            }
            break;
            case FallingPlatformState.Recovery:
            currentRecoveryTime -= Time.deltaTime;
            if(currentRecoveryTime <= 0)
            {
                //Play the reset animation
                //Set the state back to normal
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
