using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DoubleJumpAbility : Ability
{
    private float doubleJumpForce = 5f;

    [SerializeField]
    [Range(1, 5)]
    private float doubleJumpForceMultiplier = 1.5f;

    public override void Activate(GameObject parent)
    {
        DoubleJumpAbilityHolder doubleJumpAbilityHolder = parent.GetComponent<DoubleJumpAbilityHolder>();
        PlayerController player = parent.GetComponent<PlayerController>();

        PlayerJumping jumping = parent.GetComponent<PlayerJumping>();

        if(doubleJumpAbilityHolder.canDoubleJump && jumping.coyoteTimeCounter <= 0f)
        {
            player.velocity.y = Mathf.Sqrt(doubleJumpForce * doubleJumpForceMultiplier * -3 * Physics.gravity.y);
            doubleJumpAbilityHolder.canDoubleJump = false;
        }
    }

    public override void Deactivate(GameObject parent)
    {
        //PlayerController player = parent.GetComponent<PlayerController>();
        //player.SetBaseModifiers();
    }
}
