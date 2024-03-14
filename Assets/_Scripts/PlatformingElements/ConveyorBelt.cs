using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float beltSpeed;
    [SerializeField] bool isBeltActive;
    [SerializeField] bool beltMoveLeft;


    void OnTriggerStay(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        Rigidbody rb = other.GetComponent<Rigidbody>();
        //If the player is in the trigger zone
        if(player != null)
        {
            //Store the belt velocity, whatever it is set to based on the speed
            Vector3 beltVelocity = transform.forward * beltSpeed;
            if(beltMoveLeft)
            {
                beltVelocity = transform.forward * -1 * beltSpeed;
            }

            if(isBeltActive)
            {
            player.character.Move(beltVelocity * Time.fixedDeltaTime);
            }

            
        }

        if(rb != null)
        {            Vector3 beltVelocity = transform.forward * beltSpeed;
            if(beltMoveLeft)
            {
                beltVelocity = transform.forward * -1 * beltSpeed;
            }

            if(isBeltActive)
            {
                rb.velocity = beltVelocity;
            }
}
    }
}
