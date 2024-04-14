using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float controllerSensitivity = 1f;
    public float mouseSensitivity = 2f;
    public Transform target;
    public float CameraHeight = 2.0f;
    public float PlayerDistance = 5.0f;
    public float Damping = 10.0f;
    public float MinimumDistance = 2.0f; // Minimum distance
    public float MaximumDistance = 10.0f;
    public LayerMask collisionLayers;
    private Vector3 offset;
    public void Start()
    {
        offset = new Vector3(0, CameraHeight, -PlayerDistance);
    }
    private void Update()
    {
        // Check if input is coming from a mouse
        if (Mouse.current != null && Mouse.current.delta.IsActuated())
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = mouseSensitivity;
            freeLookCamera.m_YAxis.m_MaxSpeed = mouseSensitivity;
        }
        // Check if input is coming from a controller
        else if (Gamepad.current != null && Gamepad.current.allControls.Count > 0)
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = controllerSensitivity;
            freeLookCamera.m_YAxis.m_MaxSpeed = controllerSensitivity;
        }
    
    }
        private void LateUpdate()
    {
        // Calculate position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Camera collision
        RaycastHit collision;
        if (Physics.Linecast(target.position, desiredPosition, out collision, collisionLayers))
        {
            PlayerDistance = Mathf.Clamp(collision.distance, MinimumDistance, MaximumDistance);
        }
        else
        {
            PlayerDistance = MaximumDistance;
        }


        // Adjust position with new distance
        desiredPosition = target.position + target.TransformDirection(new Vector3(0, CameraHeight, -PlayerDistance));

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * Damping);

        // Look at target
        transform.LookAt(target);
    }
}