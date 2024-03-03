using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu]
public class GlideAbility : Ability
{
    [Description("Must be a value LESS THAN 1")]
    [Range(0.1f, 1f)]
    float glideMassReduction;
    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        player.massModifier *= glideMassReduction;
    }
}
