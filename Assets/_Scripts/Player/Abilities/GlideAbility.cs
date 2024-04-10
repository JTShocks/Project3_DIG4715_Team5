using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu]
public class GlideAbility : Ability
{
    [Description("Must be a value LESS THAN 1")]
    [Range(0.01f, 0.1f)]
    public float glideFallspeedReduction;
    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        if(!player.isGrounded)
        {
            //player.velocity.y = 0;
            player.fallingSpeedMultiplier *= glideFallspeedReduction;
            //player.velocity.y *= glideFallspeedReduction;
        }

        
    }
    public override void Deactivate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        player.fallingSpeedMultiplier =1;
    }
}
