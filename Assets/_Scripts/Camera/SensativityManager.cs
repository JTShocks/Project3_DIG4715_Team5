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
}