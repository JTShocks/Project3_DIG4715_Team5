using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    //TO:DO
    /*
        
    */
    [SerializeField] bool enableDebugMessages;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    //This is the primary player controller that holds a lot of the base properties for the player
    CharacterController character;

    [SerializeField] Transform cameraTransform;
    private float cameraYRotation;
    internal Vector3 moveInput;
    internal Vector3 moveDirection;
    internal Vector3 velocity;

    [Header("Player Statistics")]
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float playerMovementSpeed;
    [Description("Turning speed in how many degrees the player should rotate per second")]
    [SerializeField] float playerTurningSpeed;
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
        cameraYRotation = cameraTransform.eulerAngles.y;
        Debug.Log(cameraYRotation);

    }
    void Update()
    {
        moveInput = GetMovementInput();
        ChangeLookDirection(moveInput);
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

        moveDirection = transform.forward * moveInput.magnitude * playerMovementSpeed * movementSpeedMultiplier;


        //Calculate the rate the player should move each frame
        var factor = acceleration * Time.fixedDeltaTime;
        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z, factor);

        character.Move(velocity * Time.fixedDeltaTime);
    }

    Vector3 GetMovementInput()
    {        
        //Read in the input values to determine where the 
        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3(moveInput.x, 0, moveInput.y);

        input = Vector3.ClampMagnitude(input, 1f);
        return input;
    }

    void ChangeLookDirection(Vector3 input)
    {

        //Rotate the player to the direction of their input
        if(input != Vector3.zero)
        {

            //This is a matrix that is based relative to the angle of the camera, so that when pressing UP, you consistently move up
            //TO:DO Get the component of the Main Camera rotation, so this is consistent
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,cameraYRotation,0));
            var skewedInput = matrix.MultiplyPoint3x4(input);

            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            //We base the rotation on the relative relationship of the input to the transform
            //Rotate around the Y axis, then set the rotation only if the input is happening.

            transform.rotation = rot;
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
