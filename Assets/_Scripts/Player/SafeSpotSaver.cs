using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpotSaver : MonoBehaviour
{
    public float checkInterval = 3f;

    public LayerMask groundLayer;
    public LayerMask platformLayer;
    public LayerMask conveyorBeltLayer;

    private Vector3 lastSafePosition;
    private float nextCheckTime;
    //private Rigidbody playerRigidbody; // CharacterController could go here if using that instead.
    private CharacterController characterController;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Get stuff.
        //playerRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
        lastSafePosition = transform.position;
        nextCheckTime = Time.time + checkInterval;

        groundLayer &= ~(1 << LayerMask.NameToLayer("Platform"));
    }

    void FixedUpdate()
    {
        if (Time.time >= nextCheckTime && characterController.velocity.y <= 0)
        {
            Debug.Log("IsGrounded: " + IsGrounded() + ", HasGroundAround: " + HasGroundAround());
            if (IsGrounded() && HasGroundAround() && !IsStandingOnPlatform())
            {
                lastSafePosition = transform.position;
                Debug.Log("Saved position: " + lastSafePosition);
            }
            nextCheckTime = Time.time + checkInterval;
        }
    }
    /*
    // If collide with spike, reset position of player to last safe position.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spike Hazard"))
        {
            playerRigidbody.position = lastSafePosition;
            // Prevent the player from being reset while keeping any fast speed from prior movement.
            playerRigidbody.velocity = Vector3.zero;
        }
    }
    */
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Spike Hazard"))
        {
            //playerHealth.TakeDamage(1);
            if (PlayerHealth.CurrentHealth > 0)
            {
                characterController.enabled = false;
                Vector3 respawnPosition = lastSafePosition;
                respawnPosition.y += 1f;
                transform.position = respawnPosition;
                characterController.enabled = true;
                playerHealth.TakeDamage(1);
            }
            else
            {
                playerHealth.TakeDamage(1);
                RespawnManager respawnManager = FindObjectOfType<RespawnManager>();
                respawnManager.RespawnPlayer(gameObject);
            }
        }
    }
    /*
    // Check if the player is indeed grounded. Raycast is used.
    bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            return true;
        }
        return false;
    }
    */
    bool IsGrounded()
    {
        return characterController.isGrounded;
    }

    // Checks for ground around the player.
    bool HasGroundAround()
    {
        // Raycast is used.
        // 45 Degree angle diagonally downward in 4 directions.
        RaycastHit hit;
        Vector3[] directions = { Vector3.forward, Vector3.left, Vector3.right, Vector3.back };
        float diagonalDownwardAngle = 89.5f;
        float spikeHazardCheckDistance = 5f;
        foreach (Vector3 dir in directions)
        {
            // Cross product of the up vector and direction vector for a result that is a vector that is perpendicular to both the up vector and direction vector.
            Vector3 diagonalDirection = Quaternion.AngleAxis(diagonalDownwardAngle, Vector3.Cross(Vector3.up, dir)) * dir;
            if (!Physics.Raycast(transform.position, diagonalDirection, out hit, 0.5f, groundLayer))
            {
                return false;
            }
            if (Physics.Raycast(transform.position, diagonalDirection, out hit, spikeHazardCheckDistance))
            {
                if (hit.collider.CompareTag("Spike Hazard"))
                {
                    return false;
                }
            }
        }
        return true;
    }
    /*
    bool IsStandingOnPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.CompareTag("Platform") || hit.collider.CompareTag("ConveyorBelt"))
            {
                return true;
            }
        }
        return false;
    }
    */
    bool IsStandingOnPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, platformLayer | conveyorBeltLayer))
        {
            return true;
        }
        return false;
    }
}