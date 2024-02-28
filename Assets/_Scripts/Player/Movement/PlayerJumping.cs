using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
[SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpPressBufferTime = .05f;
    PlayerController player;
    bool isTryingToJump;
    float lastJumpPressTime;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    void OnEnable(){ player.OnBeforeMove += OnBeforeMove;}

    void OnDisable(){player.OnBeforeMove -= OnBeforeMove;}

    void OnJump()
    {
        isTryingToJump = true;
        lastJumpPressTime = Time.time;
    }

    void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;

        bool isOrWasTryingToJump = isTryingToJump || wasTryingToJump;
        if(isOrWasTryingToJump && player.isGrounded)
        {
            player.velocity.y += jumpForce;
        }
        isTryingToJump = false;
    }


}
