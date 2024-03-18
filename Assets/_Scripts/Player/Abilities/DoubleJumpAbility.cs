using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu]
public class DoubleJumpAbility : Ability
{
    private float doubleJumpForce = 5f;

    private DoubleJumpAbilityHolder doubleJumpAbilityHolder;

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        if(!player.isGrounded && doubleJumpAbilityHolder.canDoubleJump)
        {
            player.velocity.y += doubleJumpForce;
            doubleJumpAbilityHolder.canDoubleJump = false;
        }
    }

    public override void Deactivate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        player.SetBaseModifiers();
    }
}
