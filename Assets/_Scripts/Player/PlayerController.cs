using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    //TO:DO
    /*
        Adjust the player turning to ONLY do the smooth turn when the new input is less than 90 degrees
            Should have snappy change
    */
    [SerializeField] bool enableDebugMessages;
    enum MessageType{
        Default,
        Warning,
        Error
    }
    //This is the primary player controller that holds a lot of the base properties for the player
    internal CharacterController character;
    public static Transform playerTransform;

    [SerializeField] Transform cameraTransform;
    private float cameraYRotation;
    internal Vector3 moveInput;
    internal Vector3 moveDirection;
    internal Vector3 velocity;

    [Header("Player Statistics")]
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float playerMovementSpeed;
    [SerializeField] float fallSpeedCap;
    [Description("Turning speed in how many degrees the player should rotate per second")]
    [SerializeField] float playerTurningSpeed;
    internal float movementSpeedMultiplier;
    internal float massModifier;
    internal float fallingSpeedMultiplier;
    internal float turnSpeedMultiplier;

    internal bool facingIsLocked = false;

    //References to the various input actions for the player
    PlayerInput playerInput;
        InputAction moveAction;

    [SerializeField] float currentFallingSpeed;

    internal bool isGrounded => CheckForGrounded();
    float lastGroundedTime;

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
        SetBaseModifiers();

    }
    void Update()
    {
        playerTransform = transform;
        moveInput = GetMovementInput();
        ChangeLookDirection(moveInput);
        currentFallingSpeed = velocity.y;
    }

    void FixedUpdate()
    {
        //All movement calculations should be in FixedUpdate, since we are mostly dealing with rigidbodies

        UpdateGravity();
        UpdateMovement();
    }

    void UpdateGravity()
    {
        //TO:DO
        /*
            Find a good way to cap the player's falling speed to smooth out movement and to work with the glide ability
        */
        var gravity =  mass * massModifier * Time.fixedDeltaTime * Physics.gravity;
        velocity.y = isGrounded? - 1f : velocity.y + gravity.y;
        velocity.y = Mathf.Clamp(velocity.y, -fallSpeedCap * fallingSpeedMultiplier, velocity.y);
    }
    bool CheckForGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void UpdateMovement()
    { 
        moveDirection = transform.forward * moveInput.magnitude * playerMovementSpeed * movementSpeedMultiplier;
        //Calculate the rate the player should move each frame

       
        //Check to see if anything else should happer before moving
        OnBeforeMove?.Invoke();

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

            //We first compare the current rotation angle to the desired rotation angle. Since we want the player to instantly change direction when the flick the stick to turn around

            var angle = Quaternion.Angle(transform.rotation, rot);
            if(angle > 89)
            {
                if(!facingIsLocked)
                {
                //If the new angle is greater than 90 degrees, we can assume the player meant to switch direction
                    transform.rotation = rot;
                }

            } 
            else
            {
                //We want the smooth motion from the moving around
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, playerTurningSpeed*turnSpeedMultiplier * Time.deltaTime);
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

    public void SetBaseModifiers()
    {
        movementSpeedMultiplier = 1f;
        massModifier = 1f;
        fallingSpeedMultiplier = 1f;
        turnSpeedMultiplier = 1f;
    }
    

}
