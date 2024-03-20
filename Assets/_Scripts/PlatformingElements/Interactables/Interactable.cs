using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

   public Action OnInteract;
   //An interactable is assigned to a given object and subscribes to the event on a case by case basis. It isn't static since it is case by case and directly assigned

   //Generic class for interactable objects, just to keep them in one place
   public virtual void Interact()
   {
         OnInteract?.Invoke();
   }


}
