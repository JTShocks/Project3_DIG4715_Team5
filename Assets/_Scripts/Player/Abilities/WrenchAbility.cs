using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WrenchAbility : Ability
{
    public float damage;
    public Vector3 hitboxPosition;
    public float hitboxSize;
    public override void Activate(GameObject parent)
    {
        //When this activates, call the animator on the player to play the attack animation
        //The player y velocity is 0 during the swing

        PlayerController player = parent.GetComponent<PlayerController>();
        //Animator animator = parent.GetComponent<Animator>();
        //SetTrigger
    }
}
