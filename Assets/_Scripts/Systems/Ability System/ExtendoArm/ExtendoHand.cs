using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ExtendoHand : MonoBehaviour
{

    Rigidbody rb;
    CapsuleCollider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        //Set the size and shape of the collider for the hand
    }

    void OnTriggerEnter(Collider collider)
    {
        //On trigger enter, check if the thing is either an enemy or a hand hold

    }

    //It is basically a projectile that moves through the air and then 
}
