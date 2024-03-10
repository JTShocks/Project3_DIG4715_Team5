using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LegAbilityHolder : MonoBehaviour
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
    [SerializeField] Ability legAbility;
        float abilityActiveTime;
        float abilityCooldownTime;

    AbilityState state = AbilityState.Ready;
    void Awake(){
        player = GetComponent<PlayerController>();
    }

    void OnEnable(){player.OnBeforeMove += OnBeforeMove;}
    void OnDisable(){player.OnBeforeMove -= OnBeforeMove;}

    void OnBeforeMove()
    {
        if(legAbility== null)
        {
            return;
        }
        if(player.isGrounded)
        {
            //When the player is grounded, the ability is Ready again
            state = AbilityState.Ready;
            DebugMessage(legAbility.name + " is now ready.", MessageType.Default);
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
                    abilityCooldownTime = legAbility.cooldownTime;
                    DebugMessage(legAbility.name + " is on cooldown.", MessageType.Default);
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
                abilityActiveTime = legAbility.activeTime;
            }
            if(abilityActiveTime > 0)
                legAbility.Activate(gameObject);
                DebugMessage(legAbility.name + " has been activated.", MessageType.Default);
        }
        else 
        {
            legAbility.Deactivate(gameObject);
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
