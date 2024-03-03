using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAbilityHolder : MonoBehaviour
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
    [SerializeField] Ability handAbility;
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
        if(handAbility == null)
        {
            return;
        }
        switch(state)
        {
            case AbilityState.Active:
                handAbility.Activate(gameObject);
                if(abilityActiveTime > 0)
                {
                    abilityActiveTime -= Time.fixedDeltaTime;
                }
                else
                {
                    state = AbilityState.Cooldown;
                    abilityCooldownTime = handAbility.cooldownTime;
                    DebugMessage(handAbility.name + " is on cooldown.", MessageType.Default);
                }
            break;
            case AbilityState.Cooldown:
                if(abilityCooldownTime > 0)
                {
                    abilityCooldownTime -= Time.fixedDeltaTime;
                }
                else
                {
                    state = AbilityState.Ready;
                    DebugMessage(handAbility.name + " is now ready.", MessageType.Default);
                }
            break;
            default:
            break;
        }
        

    }
    
    void OnDash(InputValue value)
    {
        if(state == AbilityState.Ready && value.isPressed)
        {
            //handAbility.Activate(gameObject);
            state = AbilityState.Active;
            abilityActiveTime = handAbility.activeTime;
            DebugMessage(handAbility.name + " has been activated.", MessageType.Default);
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
