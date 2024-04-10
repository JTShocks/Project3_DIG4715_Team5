using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoubleJumpAbilityHolder : MonoBehaviour
{
    PlayerController player;

    public bool canDoubleJump = true;

    [SerializeField] bool enableDebugMessages;
    enum MessageType
    {
        Default,
        Warning,
        Error
    }

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    [Header("Current Equipped Ability")]
    [SerializeField] Ability jumpAbility;
        float abilityActiveTime;
        float abilityCooldownTime;
    static bool abilityIsEnabled = false;

    AbilityState state = AbilityState.Ready;
    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void OnEnable() { player.OnBeforeMove += OnBeforeMove; AbilityController.OnEnableAbility += SetActiveAbility; }
    void OnDisable() { player.OnBeforeMove -= OnBeforeMove; AbilityController.OnEnableAbility -= SetActiveAbility; }

    void OnBeforeMove()
    {
        if (!abilityIsEnabled)
            return;
        if (player.isGrounded)
        {
            state = AbilityState.Ready;
            canDoubleJump = true;
            DebugMessage(jumpAbility.name + " is now ready.", MessageType.Default);
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
                    //player.SetBaseModifiers();
                    state = AbilityState.Cooldown;
                    abilityCooldownTime = jumpAbility.cooldownTime;
                    jumpAbility.Deactivate(gameObject);
                    DebugMessage(jumpAbility.name + " is on cooldown.", MessageType.Default);
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

    void OnJump(InputValue value)
    {
        if(state == AbilityState.Ready && value.isPressed)
        {
            jumpAbility.Activate(gameObject);
            //state = AbilityState.Active;
            abilityActiveTime = jumpAbility.activeTime;
            DebugMessage(jumpAbility.name + " has been activated.", MessageType.Default);
        }
    }

    void SetActiveAbility(Ability ability)
    {
        if(ability.abilitySlot == jumpAbility.abilitySlot)
        {
            if (jumpAbility == ability)
                abilityIsEnabled = (jumpAbility == ability);
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
