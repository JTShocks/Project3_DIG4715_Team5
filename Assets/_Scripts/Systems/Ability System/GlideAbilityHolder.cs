using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlideAbilityHolder : MonoBehaviour
{
    [SerializeField] bool enableDebugMessages;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    PlayerController player;
    //This script is attached to the Player object and holds reference to the active ability in each slot
    enum AbilityState{
        Ready,
        Active,
        Cooldown
    }

    [Header("Current Equipped Ability")]
    [SerializeField] Ability glideAbility;
        float abilityActiveTime;
        float abilityCooldownTime;
    static bool abilityIsEnabled = false;

    AbilityState state = AbilityState.Ready;
    void Awake(){
        player = GetComponent<PlayerController>();
    }

    void OnEnable(){player.OnBeforeMove += OnBeforeMove; AbilityController.OnAbilityEquip += SetActiveAbility;}
    void OnDisable(){player.OnBeforeMove -= OnBeforeMove; AbilityController.OnAbilityEquip -= SetActiveAbility;}

    void OnBeforeMove()
    {
        if(!abilityIsEnabled)
        {
            return;
        }
        if(glideAbility== null)
        {
            return;
        }

        if(player.isGrounded)
        {
            //When the player is grounded, the ability is Ready again
            state = AbilityState.Ready;
            DebugMessage(glideAbility.name + " is now ready.", MessageType.Default);
        }
        switch(state)
        {
            case AbilityState.Active:
                if(abilityActiveTime > 0)
                {
                    abilityActiveTime -= Time.fixedDeltaTime;
                }
                else
                {
                    player.SetBaseModifiers();
                    state = AbilityState.Cooldown;
                    abilityCooldownTime = glideAbility.cooldownTime;
                    DebugMessage(glideAbility.name + " is on cooldown.", MessageType.Default);
                }
            break;
            case AbilityState.Cooldown:

            break;
            default:
            break;
        }
        

    }
    
    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {            

            if(state == AbilityState.Ready)
            {
                state = AbilityState.Active;
                abilityActiveTime = glideAbility.activeTime;
            }
            if(abilityActiveTime > 0)
                glideAbility.Activate(gameObject);
                DebugMessage(glideAbility.name + " has been activated.", MessageType.Default);
        }
        else 
        {
            glideAbility.Deactivate(gameObject);
        }
    }
    void SetActiveAbility(Ability ability)
    {
        Debug.Log("Event was triggered");
        if(ability.abilitySlot == glideAbility.abilitySlot)
        {
            if(glideAbility == ability)
            {
                abilityIsEnabled = true;
            }
            else
            {
                abilityIsEnabled = false;
            }
        }
    }

    void DebugMessage(string message, MessageType messageType)
    {
        if(enableDebugMessages)
        {
            switch(messageType)
            {
                case MessageType.Default:
                Debug.Log(message);
                break;
                case MessageType.Warning:
                Debug.LogWarning(message);
                break;
                case MessageType.Error:
                Debug.LogError(message);
                break;
            }

        }
    }
    
}
