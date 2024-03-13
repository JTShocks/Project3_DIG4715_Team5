using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Pickup : MonoBehaviour
{
    // This is the base script that the pickups will inherit from.
    // They all work on the "on trigger enter" and all destroy themselves if they can


    public virtual void OnPickup()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPickup();
        }
    }


}
