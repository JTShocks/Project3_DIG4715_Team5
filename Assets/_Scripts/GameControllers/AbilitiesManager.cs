using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance {get; private set;}

    public static Action<Ability> OnUnlockAbility;

    [Header("Player Unlocked Abilities")]
    // Holds a list of all the unlocked abilities, unsorted.
    public List<Ability> unlockedAbilities;

    //This dictionary contains the equipped abilities
    //It is defined with each possible slot set to empty
    //When an ability is equipped, the ability is assigned to the specific slot
    //They all start as empty
    public Dictionary<AbilitySlot, Ability> equippedAbilities = new Dictionary<AbilitySlot, Ability>{
        {AbilitySlot.Core, null},
        {AbilitySlot.Hand, null},
        {AbilitySlot.Leg, null},
        {AbilitySlot.Weapon, null},
        };

    //The UI will read this list when comparing the abilities to their respective spots
    //The AbilityController will handle actually activating the ability.


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //This function unlocks the ability and send out an event when an ability is unlocked
    void UnlockAbility(Ability ability)
    {
        if(!unlockedAbilities.Contains(ability))
        {
            unlockedAbilities.Add(ability);
            OnUnlockAbility?.Invoke(ability);
        }
    }

    //Methods for equipping and dequipping abilities
    public void EquipAbility(Ability ability)
    {
        //Always clear the space before trying to equip an ability
        ClearAbilitySlot(ability.abilitySlot);
        equippedAbilities[ability.abilitySlot] = ability;
    }

    public void ClearAbilitySlot(AbilitySlot slotToClear)
    {
        // Sets the value of the space to null or empty, 
        equippedAbilities[slotToClear] = null;
    }




}
