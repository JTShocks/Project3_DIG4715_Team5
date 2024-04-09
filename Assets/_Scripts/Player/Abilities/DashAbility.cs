using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;
    public float turningSpeedModifier;

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        
        player.velocity = player.transform.forward * dashVelocity;
        player.turnSpeedMultiplier = turningSpeedModifier;
        player.facingIsLocked = true;
    }

    public override void Deactivate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        player.turnSpeedMultiplier = 1;
        player.facingIsLocked = false;
    }
}
