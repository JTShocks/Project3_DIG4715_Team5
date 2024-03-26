using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraController : MonoBehaviour
{
    public Transform target; // Reference to the player or object the camera should follow
    public float followSpeed = 5f; // Speed at which the camera follows the target

    void Update()
    {
        // Ensure the target is not null
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to the camera.");
            return;
        }

        // Get the current position of the camera
        Vector3 newPosition = transform.position;

        // Only update the horizontal position of the camera
        newPosition.x = target.position.x;

        // Apply smoothing and update the camera's position
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}