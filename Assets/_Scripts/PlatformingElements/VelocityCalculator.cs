using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityCalculator : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Vector3 _velocity;

    private Rigidbody rb;

    private void Awake()
    {

       rb = GetComponent<Rigidbody>();
                _previousPosition = rb.position;
    }

    void FixedUpdate()
    {
        _velocity = (rb.position - _previousPosition) / Time.fixedDeltaTime;
        _previousPosition = rb.position;
    }

    public Vector3 GetVelocity()
    {
        return _velocity;
    }
}
