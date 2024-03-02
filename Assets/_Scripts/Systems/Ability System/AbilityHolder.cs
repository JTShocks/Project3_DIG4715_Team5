using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{

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
        switch(state)
        {
            case AbilityState.Active:
                if(abilityActiveTime > 0)
                {
                    abilityActiveTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.Cooldown;
                    abilityCooldownTime = handAbility.cooldownTime;
                }
            break;
            case AbilityState.Cooldown:
                if(abilityCooldownTime > 0)
                {
                    abilityCooldownTime -= Time.deltaTime;
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
    
    void OnDash()
    {
        if(state == AbilityState.Ready)
        {
            handAbility.Activate(gameObject);
            state = AbilityState.Active;
            abilityActiveTime = handAbility.activeTime;
        }
    }
}
