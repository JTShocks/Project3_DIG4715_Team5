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


     [Header("Current Equipped Ability")]
    [SerializeField] Ability extendArmAbility;
        float abilityActiveTime;
        float abilityCooldownTime;
    static bool abilityIsEnabled = false;
    AbilityState state = AbilityState.Ready;

    //These are the variables specific to the extendoArm

    Rigidbody handRB;
    internal Vector3 handDestination;
    internal float handRetractSpeed;

    [SerializeField] GameObject extendoHand;

    Vector3 dropPointDirection;
    Vector3 dropPoint;

    bool foundGrapplePoint = false;

    ExtendoHand activeHand;
    GameObject hand;

    void Awake(){
        player = GetComponent<PlayerController>();
    }
    void OnEnable(){
        player.OnBeforeMove += OnBeforeMove;
        AbilityController.OnEnableAbility += SetActiveAbility;
        ExtendoHand.OnReachHandHold += SetDestinationPoint;
        ExtendoHand.OnRetracted += OnRetracted;
    }
    void OnDisable(){
        player.OnBeforeMove -= OnBeforeMove;
        AbilityController.OnEnableAbility -= SetActiveAbility;
        ExtendoHand.OnReachHandHold -= SetDestinationPoint;
        ExtendoHand.OnRetracted -= OnRetracted;
    }

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
            //While this is active, the player is locked in place and has no input
            //Only when the hand begins moving, then it will continue.


                if(activeHand.isRetracting)
                {
                    if(foundGrapplePoint)
                    {
                        PullPlayerToGrapplePoint();
                    }
                    else
                    {
                        player.velocity = Vector3.zero;
                        activeHand.Retract(transform.position);
                    }
                }
                else
                {
                    player.velocity = Vector3.zero;
                        activeHand.Launch(handDestination);

                    

                }
                //Hand will spawn at the transform forward in local position, 1 space away.
            //handRB.position = Vector3.MoveTowards()
            //When the ability is active, it is moving the hand to the desired location
            break;
            case AbilityState.Cooldown:
                AbilityController.changeAction.Enable();

                state = AbilityState.Ready;
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

            extendArmAbility.Activate(gameObject);
            if(Physics.Raycast(transform.position, handDestination, 2))
            {
                Debug.Log("Too close to a wall");
                return;
            }
            //var ray = Physics.Raycast();

            AbilityController.changeAction.Disable();
            CreateHandHitbox();

            player.controlsLocked = true;
            player.facingIsLocked = true;
            
                        state = AbilityState.Active;



            //CreateHand()



            /*hitbox.enabled = true;
            player.playerAnimator.SetTrigger("OnDash");
            handAbility.Activate(gameObject);
            state = AbilityState.Active;
            abilityActiveTime = handAbility.activeTime;
            DebugMessage(handAbility.name + " has been activated.", MessageType.Default);*/
        }
    }

    void CreateHandHitbox()
    {
        //Create the hand
        player.velocity = Vector3.zero;
        hand = Instantiate(extendoHand);
        activeHand = hand.GetComponent<ExtendoHand>();
        activeHand.rb.position = transform.position + new Vector3(0,1,0);
            activeHand.retractSpeed = handRetractSpeed;

        //Get the necessary components for the hand
        //Subscribe to the events on the hand, then unsubscribe when the hand is not active
    }

    void OnRetracted()
    {
        state = AbilityState.Cooldown;
                        Destroy(hand);
                activeHand = null;
                player.controlsLocked = false;
                player.facingIsLocked = false;
                foundGrapplePoint = false;
    }

    void SetDestinationPoint(Vector3 dropPoint)
    {
        this.dropPoint = dropPoint;
        dropPointDirection = dropPoint - transform.position;
        foundGrapplePoint = true;


    }

    void PullPlayerToGrapplePoint()
    {
        player.velocity = new Vector3(dropPointDirection.normalized.x, 0, dropPointDirection.normalized.z) * handRetractSpeed;
        //transform.position = Vector3.MoveTowards(player.transform.position, dropPoint, Time.fixedDeltaTime);
        if(Vector3.Distance(player.transform.position, dropPoint) <= 2)
        {
            //If the player makes it to the destination, then the arm should stop retracting
            //Change the state first
            activeHand.isRetracting = false;
            OnRetracted();
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
