using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpPressBufferTime = .05f;
    PlayerController player;
    bool isTryingToJump = false;
    bool jumpIsCanceled;
    float lastJumpPressTime;

    void Awake()
    {
        player = GetComponent<PlayerController>();

    }
    void OnEnable(){ player.OnBeforeMove += OnBeforeMove;}

    void OnDisable(){player.OnBeforeMove -= OnBeforeMove;}

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            isTryingToJump = true;
            lastJumpPressTime = Time.time;
        }
        else
        {
            jumpIsCanceled = true;
        }

    }

    void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;

        bool isOrWasTryingToJump = isTryingToJump || wasTryingToJump;

        //If the player was trying to jump and the jump wasn't canceled
        if(isOrWasTryingToJump && player.isGrounded)
        {
            player.velocity.y += jumpForce;

        }

        if(jumpIsCanceled)
        {
            if(player.velocity.y > jumpForce/2)
            {
                player.velocity.y = jumpForce/2;
            }
            jumpIsCanceled = false;
        }

        isTryingToJump = false;      

    }


}
