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
    [SerializeField] float delayBeforeFalling;

    private FallingPlatformState state;

    // Start is called before the first frame update
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

            break;
            case FallingPlatformState.Recovery:

            break;
            default:
            break;
        }
    }
}
