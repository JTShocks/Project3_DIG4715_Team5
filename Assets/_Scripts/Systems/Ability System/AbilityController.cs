using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    public static Action<Ability> OnAbilityEquip;

    public List<Ability> equippedAbilities;

    void Start()
    {
        foreach (Ability ability in AbilitiesManager.instance.unlockedAbilities)
        {
            Debug.Log(ability);
            EquipAbility(ability);
        }
    }

    void EquipAbility(Ability ability)
    {
        Debug.Log("Equipping ability" + ability);
        if(OnAbilityEquip != null)
        {
            OnAbilityEquip.Invoke(ability);
        }
    }
}
