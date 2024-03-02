using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        
        player.velocity = player.moveInput.normalized * dashVelocity;
        
    }
}
