using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{


    [SerializeField] List<Ability> allAbilities;
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

    [SerializeField] GameObject playerModel;

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

    internal bool controlsLocked = false;

    //References to the various input actions for the player
    public static PlayerInput playerInput;
        InputAction moveAction;
        InputAction numKey;

    [SerializeField] float currentFallingSpeed;

    internal bool isGrounded => CheckForGrounded();
    float lastGroundedTime;

    [Range(0.1f, 1f)]
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundLayer;

    public event Action OnBeforeMove;

    [SerializeField] public Animator playerAnimator;

    void Awake()
    {
        //Get the components for the game object
        character = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        numKey = playerInput.actions["numkeys"];
        
        Debug.Log(cameraYRotation);
        SetBaseModifiers();

    }
    void Update()
    {
        cameraYRotation = cameraTransform.eulerAngles.y;
        playerTransform = transform;

        playerAnimator.SetBool("IsNotFalling", isGrounded);

        currentFallingSpeed = velocity.y;
        moveInput = Vector2.zero;
        if(!controlsLocked)
        {
                moveInput = GetMovementInput();
                ChangeLookDirection(moveDirection);
        }

        if(moveInput != Vector3.zero && isGrounded)
        {
            playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }

        
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
        playerAnimator.SetFloat("FallSpeed", velocity.y + 1);
    }
    bool CheckForGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void OnPause()
    {

            if(GameManager.currentGameState == GameManager.GameState.Paused)
            {

                GameManager.instance.ChangeGameState(GameManager.instance.previousState);
                
            }
            else
            {
                GameManager.instance.ChangeGameState(GameManager.GameState.Paused);
                playerInput.SwitchCurrentActionMap("UI");
            }
    }

    void UpdateMovement()
    { 
        moveDirection = moveInput;
        moveDirection = Quaternion.AngleAxis(cameraYRotation, Vector3.up) * moveDirection;
        moveDirection.Normalize();
        //Calculate the rate the player should move each frame

       
        //Check to see if anything else should happer before moving
        OnBeforeMove?.Invoke();

        var factor = acceleration * Time.fixedDeltaTime;

        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * playerMovementSpeed * movementSpeedMultiplier, factor);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * playerMovementSpeed * movementSpeedMultiplier, factor);

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
            /*
            //This is a matrix that is based relative to the angle of the camera, so that when pressing UP, you consistently move up
            //TO:DO Get the component of the Main Camera rotation, so this is consistent
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,cameraYRotation,0));
            var skewedInput = matrix.MultiplyPoint3x4(input);

            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
            //We base the rotation on the relative relationship of the input to the transform
            //Rotate around the Y axis, then set the rotation only if the input is happening.

            //We first compare the current rotation angle to the desired rotation angle. Since we want the player to instantly change direction when the flick the stick to turn around

            var angle = Quaternion.Angle(transform.rotation, rot);


                //We want the smooth motion from the moving around
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, playerTurningSpeed*turnSpeedMultiplier * Time.deltaTime);
            */


            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerTurningSpeed * turnSpeedMultiplier * Time.deltaTime);
            //float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref playerTurningSpeed, 0.1f);
            //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
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


    //Section for DEV Commands to teleport the player to different stages
    void OnNumKeys()
    {
           Vector3 button = numKey.ReadValue<Vector3>();

            float buttonX = button.x;
            float buttonY = button.y;
            float buttonZ = button.z;
           switch(buttonX, buttonY, buttonZ)
           {
            case (0f,1f,0f):
            //HUB
            UnityEngine.SceneManagement.SceneManager.LoadScene("Hubtest");
            break;
            case (0f,-1f,0f):
            //Level1
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1-1");
            break;
            case (-1f,0f,0f):
            //Level2
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3");
            break;
            case (1f,0f,0f):
            //Level3
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
            break;
            case (0f,0f,1f):
            //Tutorial
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
            break;
            case (0f,0f,-1f):
            foreach(Ability ability in allAbilities)
            {
                AbilitiesManager.instance.UnlockAbility(ability);
            }

            break;
           }
    }
    

}
