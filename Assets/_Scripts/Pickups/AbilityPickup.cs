using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : Pickup
{
    [SerializeField] Ability abilityToPickup;

    public override void OnPickup()
    {

        AbilitiesManager.instance.UnlockAbility(abilityToPickup);
        base.OnPickup();
    }


}
