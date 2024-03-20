using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpotSaver : MonoBehaviour
{
    public float checkInterval = 3f;
    public LayerMask groundLayer;
    private Vector3 lastSafePosition;
    private float nextCheckTime;
    private Rigidbody playerRigidbody; // CharacterController could go here if using that instead.

    // Start is called before the first frame update
    void Start()
    {
        // Get stuff.
        playerRigidbody = GetComponent<Rigidbody>();
        lastSafePosition = transform.position;
        nextCheckTime = Time.time + checkInterval;
    }

    void FixedUpdate()
    {
        if(Time.time >= nextCheckTime)
        {
            Debug.Log("IsGrounded: " + IsGrounded() + ", HasGroundAround: " + HasGroundAround());
            if (IsGrounded() && HasGroundAround())
            {
                lastSafePosition = transform.position;
                Debug.Log("Saved position: " + lastSafePosition);
            }
            nextCheckTime = Time.time + checkInterval;
        }
    }

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

    // Checks for ground around the player.
    bool HasGroundAround()
    {
        // Raycast is used.
        // 45 Degree angle diagonally downward in 4 directions.
        RaycastHit hit;
        Vector3[] directions = { Vector3.forward, Vector3.left, Vector3.right, Vector3.back };
        float diagonalDownwardAngle = 45f;
        foreach (Vector3 dir in directions)
        {
            // Cross product of the up vector and direction vector for a result that is a vector that is perpendicular to both the up vector and direction vector.
            Vector3 diagonalDirection = Quaternion.AngleAxis(diagonalDownwardAngle, Vector3.Cross(Vector3.up, dir)) * dir;
            if(!Physics.Raycast(transform.position, diagonalDirection, out hit, 1f, groundLayer))
            {
                return false;
            }
        }
        return true;
    }
}
