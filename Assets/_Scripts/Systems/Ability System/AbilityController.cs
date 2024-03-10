using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    public static Action<Ability> OnEnableAbility;

    public List<Ability> equippedAbilities;

    void OnEnable(){AbilitiesManager.OnEquipAbility += EnableAbility;}
    void OnDisable(){AbilitiesManager.OnEquipAbility -= EnableAbility;}

    void Awake()
    {
        if(equippedAbilities.Count > 0)
        {
            foreach(Ability ability in equippedAbilities)
            {
                EnableAbility(ability);
            }
        }
    }

    void EnableAbility(Ability ability)
    {
        Debug.Log("Equipping ability" + ability);
        if(OnEnableAbility != null)
        {
            OnEnableAbility.Invoke(ability);
        }
    }
}
