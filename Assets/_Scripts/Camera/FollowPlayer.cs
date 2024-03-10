using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 positionOffset;
    [SerializeField] Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;

    Vector3 targetPosition;

    void Awake()
    {
        positionOffset = transform.position - target.position;
    }

    void FixedUpdate()
    { 
       targetPosition = target.position + positionOffset;
       transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        
    }
}
