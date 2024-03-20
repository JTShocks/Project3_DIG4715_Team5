using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistHitbox : Hitbox
{

    public override void OnTriggerEnter(Collider other)
    {

        Interactable i = other.GetComponent<Interactable>();
        if(i != null)
        {
            i.Interact();
        }

        base.OnTriggerEnter(other);
    }
}
