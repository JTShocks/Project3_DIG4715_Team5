using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WrenchAbilityHolder : MonoBehaviour
{
[SerializeField] bool enableDebugMessages;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    PlayerController player;
    //This script is attached to the Player object and holds reference to the active ability in each slot
    internal enum AbilityState{
        Ready,
        Active,
        Cooldown
    }

    [Header("Current Equipped Ability")]
    [SerializeField] Ability wrenchAbility;
        float abilityActiveTime;
        float abilityCooldownTime;
    static bool abilityIsEnabled = false;

    internal AbilityState state = AbilityState.Ready;
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
            case AbilityState.Active:
                
                if(player.velocity.y < 0)
                    player.fallingSpeedMultiplier = 0;
                    player.velocity.y = 0;
                if(abilityActiveTime > 0)
                {
                    abilityActiveTime -= Time.fixedDeltaTime;
                }
                else
                {
                    player.SetBaseModifiers();
  
                    wrenchAbility.Deactivate(gameObject);
                    state = AbilityState.Cooldown;
                    abilityCooldownTime = wrenchAbility.cooldownTime;
                    DebugMessage(wrenchAbility.name + " is on cooldown.", MessageType.Default);
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
                }
            break;
            default:
            break;
        }
        

    }
    
    
    void OnFire(InputValue value)
    {
        if(!abilityIsEnabled)
        {
            return;
        }
        if(value.isPressed)
        {            

        }
        if(state == AbilityState.Ready)
        {

        state = AbilityState.Active;
        Debug.Log("Swung the wrench");
                wrenchAbility.Activate(gameObject);
        }



    }
    void SetActiveAbility(Ability ability)
    {
        if(ability.abilitySlot == wrenchAbility.abilitySlot)
        {
            if(wrenchAbility == ability)
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