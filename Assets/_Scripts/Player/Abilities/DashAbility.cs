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
        
        player.velocity = player.moveDirection * dashVelocity;
        player.turnSpeedMultiplier = turningSpeedModifier;
        player.facingIsLocked = true;
    }

    public override void Deactivate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        player.SetBaseModifiers();
        player.facingIsLocked = false;
    }
}
