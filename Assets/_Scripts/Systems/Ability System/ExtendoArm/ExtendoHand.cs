using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ExtendoHand : MonoBehaviour
{

    public static Action<Vector3> OnReachHandHold;
    public static Action OnReachEndOfLine;
    public static Action OnRetracted;

    internal Rigidbody rb;
    CapsuleCollider col;

    internal float retractSpeed;
    internal bool isRetracting = false;

    internal Vector3 startPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        //Set the size and shape of the collider for the hand

        startPoint = transform.position;
    }

    public void Launch(Vector3 destination)
    {
        rb.position = Vector3.MoveTowards(rb.position, destination, retractSpeed * Time.fixedDeltaTime);
        if(Vector3.Distance(rb.position, destination) <= 0.8)
        {
            OnReachEndOfLine?.Invoke();
            isRetracting = true;
            col.enabled = false;
        }
    }

    public void Retract(Vector3 destination)
    {
        rb.position = Vector3.MoveTowards(rb.position, destination, retractSpeed * Time.fixedDeltaTime);
        if(Vector3.Distance(rb.position, destination) <= 1)
        {
            OnRetracted?.Invoke();
            isRetracting = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        //Use a collision and just leave out anything it should ignore/ check for certain tags

            Vector3 point = collision.GetContact(0).point;
            OnReachHandHold?.Invoke(point);
            isRetracting = true;

        

    }


    //It is basically a projectile that moves through the air and then 
}
