using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExtendoArmAbility : Ability
{

    //How does the ability work?
    // Player remains stalled in air until the hand retracts (which is when the collider reaches the terminal destination)
    // After it is done retracting, the player will begin falling at normal speed.
    // 

    //Instantiate a "grapple hitbox"
    //If it collides with anything, handle the collision
    //Deactivate = delete the spawned box and return player movement to normal
    //Lock out inputs until the ability is done

    [SerializeField] float grappleArmDistance;
    [SerializeField] float grappleArmRetractSpeed;

    public override void Activate(GameObject parent)
    {

        ExtendoArmAbilityHolder holder = parent.GetComponent<ExtendoArmAbilityHolder>();
        holder.handDestination = parent.transform.position + parent.transform.forward * grappleArmDistance;
        Debug.Log(holder.handDestination);
        holder.handRetractSpeed = grappleArmRetractSpeed;

    }

    public override void Deactivate(GameObject parent)
    {
        //Destroy the hitbox
        //Begin the retraction
    }
}
