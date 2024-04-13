using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{

    public static Action<Ability> OnEnableAbility;

    public List<Ability> equippedAbilities;
    InputAction changeAction;

    void OnEnable(){AbilitiesManager.OnEquipAbility += EnableAbility;}
    void OnDisable(){AbilitiesManager.OnEquipAbility -= EnableAbility;}

    void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        changeAction = input.actions["quickswitch"];
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

    //To equip the unlocked abilities quickly
    //Up = Core ability, Left = Weapon, Right = hand, Down = Leg

    void OnQuickSwitch()
    {
        //Get the input direction and compare with a switch statement.
        //Check the list of unlocked abilities for another ability that shares the slot
        //If there isn't then just default to no ability

        var input = changeAction.ReadValue<Vector2>();
        //Get the player input to try equipping an ability

        input.Normalize();

        AbilitySlot slotToCheck = new();
        
        //Search the unlocked abilities for one of the same type
        switch(input.x)
        {
            case -1:
                //Left
                slotToCheck = AbilitySlot.Weapon;
            break;
            case 1:
                //Right
                slotToCheck = AbilitySlot.Hand;
            break;
            default:
            break;
        }
        switch(input.y)
        {
            case -1:
                //Down
                slotToCheck = AbilitySlot.Leg;
            break;
            case 1:
                //Up
                slotToCheck = AbilitySlot.Core;
            break;
                        default:
            break;
        }

        AbilitiesManager.instance.FindAbilityWithSlot(slotToCheck);

    }


}
