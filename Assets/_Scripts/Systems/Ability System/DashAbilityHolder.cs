using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbilityHolder : MonoBehaviour
{
    [SerializeField] bool enableDebugMessages;
    [SerializeField] GameObject dashFist;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    PlayerController player;
    AbilityController abilityController;
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
    static bool abilityIsEnabled = false;

    [SerializeField] Hitbox hitbox;
    AbilityState state = AbilityState.Ready;
    void Awake(){
        player = GetComponent<PlayerController>();
        dashFist.SetActive(abilityIsEnabled);

    }

    void OnEnable(){player.OnBeforeMove += OnBeforeMove; AbilityController.OnEnableAbility += SetActiveAbility;}
    void OnDisable(){player.OnBeforeMove -= OnBeforeMove; AbilityController.OnEnableAbility -= SetActiveAbility;}

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
                    handAbility.Deactivate(gameObject);
                    hitbox.box.enabled = false;
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
                    AbilityController.changeAction.Enable();
                    DebugMessage(handAbility.name + " is now ready.", MessageType.Default);
                }
            break;
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
            AbilityController.changeAction.Disable();
            hitbox.box.enabled = true;
            hitbox.damage = 2;
            player.playerAnimator.SetTrigger("OnDash");
            Animator dashFistAnimator = dashFist.GetComponent<Animator>();
            dashFistAnimator.SetTrigger("OnPunch");
            handAbility.Activate(gameObject);
            state = AbilityState.Active;
            abilityActiveTime = handAbility.activeTime;
            DebugMessage(handAbility.name + " has been activated.", MessageType.Default);
        }
    }


    void SetActiveAbility(Ability ability)
    {
        if(ability.abilitySlot == handAbility.abilitySlot)
        {
            if(ability == handAbility)
            {
                abilityIsEnabled = true;
                dashFist.SetActive(abilityIsEnabled);
                
            }
            else
            {
                abilityIsEnabled = false;
                dashFist.SetActive(abilityIsEnabled);
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
