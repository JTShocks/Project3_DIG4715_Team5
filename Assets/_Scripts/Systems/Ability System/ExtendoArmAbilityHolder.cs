using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ExtendoArmAbilityHolder : MonoBehaviour
{
    PlayerController player;
    //This script is attached to the Player object and holds reference to the active ability in each slot
    enum AbilityState{
        Ready,
        Active,
        Cooldown
    }

    bool isRetracting;

     [Header("Current Equipped Ability")]
    [SerializeField] Ability extendArmAbility;
        float abilityActiveTime;
        float abilityCooldownTime;
    static bool abilityIsEnabled = false;
    AbilityState state = AbilityState.Ready;
    void Awake(){
        player = GetComponent<PlayerController>();
    }
    void OnEnable(){player.OnBeforeMove += OnBeforeMove; AbilityController.OnEnableAbility += SetActiveAbility;}
    void OnDisable(){player.OnBeforeMove -= OnBeforeMove; AbilityController.OnEnableAbility -= SetActiveAbility;}

    void OnBeforeMove()
    {
        if(!abilityIsEnabled)
        {
            return;
        }

        switch(state)
        {
            //While the ability is active, the player remains locked in the air.
            //The player should not be able to input anything other than pause until the ability is completed.
            case AbilityState.Ready:
            break;
            case AbilityState.Active:
            break;
            case AbilityState.Cooldown:
            break;
            //While the ability is active, before it goes on cooldown, it checks if it is retracting and will wait until it is done retracting.
            default:
            break;
        }
    }

    void OnDash(InputValue value)
    {
        if(!abilityIsEnabled)
        {
            return;
        }
        if(state == AbilityState.Ready && value.isPressed)
        {
            /*hitbox.enabled = true;
            player.playerAnimator.SetTrigger("OnDash");
            handAbility.Activate(gameObject);
            state = AbilityState.Active;
            abilityActiveTime = handAbility.activeTime;
            DebugMessage(handAbility.name + " has been activated.", MessageType.Default);*/
        }
    }


    void SetActiveAbility(Ability ability)
    {
        if(ability.abilitySlot == extendArmAbility.abilitySlot)
        {
            if(extendArmAbility == ability)
            {
                abilityIsEnabled = true;
            }
            else
            {
                abilityIsEnabled = false;
            }
        }
    }
}
