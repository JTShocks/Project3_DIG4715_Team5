using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : Pickup
{

    [SerializeField] bool autoEquipOnPickup;
    [SerializeField] Ability abilityToPickup;

    public override void OnPickup()
    {

        AbilitiesManager.instance.UnlockAbility(abilityToPickup);
        base.OnPickup();

        if(autoEquipOnPickup)
        {
            AbilitiesManager.instance.EquipAbility(abilityToPickup);
        }


    }


}
