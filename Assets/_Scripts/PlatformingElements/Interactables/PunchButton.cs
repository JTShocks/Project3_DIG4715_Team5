using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchButton : Interactable
{
    //Place for the punchable button. Needs to specifically be triggered by the PunchAttack
    //Interactable can have a reference to "triggered by dash punch"
    //When the punch touches it, if it has that value set to true, it triggers the switch


    //Only the fist punch works with these buttons, so could do a direct reference


    public override void Interact()
    {
        base.Interact();
    }

    void OnTriggerEnter(Collider other)
    {
        //Interact();
    }
}
