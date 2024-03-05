using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendoArmAbility : Ability
{

    //How does the ability work?
    // Player remains stalled in air until the hand retracts (which is when the collider reaches the terminal destination)
    // After it is done retracting, the player will begin falling at normal speed.
    // 

    public override void Activate(GameObject parent)
    {
        
    }

    public override void Deactivate(GameObject parent)
    {
        
    }
}
