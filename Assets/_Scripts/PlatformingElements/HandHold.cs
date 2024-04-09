using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHold : MonoBehaviour
{
   

   void OnTriggerEnter(Collider collider)
   {
        PlayerController player = collider.GetComponent<PlayerController>();
        if(player != null)
        {
            
        }
   }
}
