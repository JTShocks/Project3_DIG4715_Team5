using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoubleJumpAbilityHolder : MonoBehaviour
{
    PlayerController player;

    public DoubleJumpAbilityHolder doubleJumpAbility;
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

    void Enable() { player.OnBeforeMove += OnBeforeMove; AbilityController.OnEnableAbility += SetActiveAbility; }
    void OnDisable() { player.OnBeforeMove -= OnBeforeMove; AbilityController.OnEnableAbility -= SetActiveAbility; }

    void OnBeforeMove()
    {
        if (!abilityIsEnabled)
            return;
        if (player.isGrounded && state != AbilityState.Ready)
        {
            state = AbilityState.Ready;
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
                    player.SetBaseModifiers();
                    state = AbilityState.Cooldown;
                    abilityCooldownTime = jumpAbility.cooldownTime;
                    DebugMessage(jumpAbility.name + " is on cooldown.", MessageType.Default);
                }
                break;
            case AbilityState.Cooldown:
                break;
            default:
                break;
        }
    }

    void OnJumpAgain(InputValue value)
    {
        UseDoubleJump();
    }

    public void UseDoubleJump()
    {
        if(state == AbilityState.Ready)
        {
            jumpAbility.Activate(gameObject);
            state = AbilityState.Active;
            DebugMessage(jumpAbility.name + " has been activated.", MessageType.Default);
        }
    }

    public void ResetDoubleJump()
    {
        if(state == AbilityState.Cooldown && player.isGrounded)
        {
            state = AbilityState.Ready;
            DebugMessage(jumpAbility.name + " has been reset.", MessageType.Default);
        }
    }

    void SetActiveAbility(Ability ability)
    {
        if(ability.abilitySlot == jumpAbility.abilitySlot)
        {
            if (jumpAbility == ability)
                abilityIsEnabled = true;
            else
                abilityIsEnabled = false;
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
