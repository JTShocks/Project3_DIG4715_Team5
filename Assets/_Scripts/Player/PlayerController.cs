using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool enableDebugMessages;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    //This is the primary player controller that holds a lot of the base properties for the player
    CharacterController character;

    Vector2 moveInput;
    Vector3 moveDirection;
    internal Vector3 velocity;

    [Header("Player Statistics")]
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float playerMovementSpeed;
    internal float movementSpeedMultiplier;

    //References to the various input actions for the player
    PlayerInput playerInput;
        InputAction moveAction;

    internal bool isGrounded => CheckForGrounded();

    [Range(0.1f, 1f)]
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundLayer;

    public event Action OnBeforeMove;

    void Awake()
    {
        //Get the components for the game object
        character = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];

    }

    void FixedUpdate()
    {
        //All movement calculations should be in FixedUpdate, since we are mostly dealing with rigidbodies
        UpdateGravity();
        UpdateMovement();
    }

    void UpdateGravity()
    {
        var gravity =  mass * Time.fixedDeltaTime * Physics.gravity;
        velocity.y = isGrounded? - 1f : velocity.y + gravity.y;
    }
    bool CheckForGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        //Check to see if anything else should happer before moving
        OnBeforeMove?.Invoke();
        var input = GetMovementInput();

        //Calculate the rate the player should move each frame
        var factor = acceleration * Time.fixedDeltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        character.Move(velocity * Time.fixedDeltaTime);
    }

    Vector3 GetMovementInput()
    {        
        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= playerMovementSpeed * movementSpeedMultiplier;
        return input;
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
