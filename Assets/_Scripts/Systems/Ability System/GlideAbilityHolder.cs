using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlideAbilityHolder : MonoBehaviour
{
    [SerializeField] bool enableDebugMessages;

    [SerializeField] GameObject glider;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    PlayerController player;
    //This script is attached to the Player object and holds reference to the active ability in each slot
    public enum AbilityState{
        Ready,
        Active,
        Cooldown
    }

    [Header("Current Equipped Ability")]
    [SerializeField] Ability glideAbility;
        float abilityActiveTime;
        float abilityCooldownTime;
    static bool abilityIsEnabled = false;

    public AbilityState state = AbilityState.Ready;
    void Awake(){
        player = GetComponent<PlayerController>();
        glider.SetActive(false);
    }

    void OnEnable(){player.OnBeforeMove += OnBeforeMove; AbilityController.OnEnableAbility += SetActiveAbility;}
    void OnDisable(){player.OnBeforeMove -= OnBeforeMove; AbilityController.OnEnableAbility -= SetActiveAbility;}

    void OnBeforeMove()
    {
        if(!abilityIsEnabled)
        {
            return;
        }
        if(player.isGrounded && state != AbilityState.Ready)
        {
            //When the player is grounded, the ability is Ready again
            state = AbilityState.Ready;
            player.playerAnimator.SetBool("IsGliding", false);
            DebugMessage(glideAbility.name + " is now ready.", MessageType.Default);
        }
        switch(state)
        {
            case AbilityState.Active:
                /*
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
                */

            break;
            case AbilityState.Cooldown:

            break;
            default:
            break;
        }
        

    }
    
    void OnUseCore(InputValue value)
    {
        if(!abilityIsEnabled)
        {
            return;
        }
        if(value.isPressed && state != AbilityState.Active)
        {            
            AbilityController.changeAction.Disable();
            glideAbility.Activate(gameObject);
            player.playerAnimator.SetBool("IsGliding", true);
            state = AbilityState.Active;
            glider.SetActive(true);
                
        }
        else 
        {
            AbilityController.changeAction.Enable();
            glideAbility.Deactivate(gameObject);
            player.playerAnimator.SetBool("IsGliding", false);
            state = AbilityState.Ready;
            glider.SetActive(false);
        }
    }
    void SetActiveAbility(Ability ability)
    {
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
