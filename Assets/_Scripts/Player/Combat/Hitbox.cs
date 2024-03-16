using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Hitbox : MonoBehaviour
{

    //Base hitbox class
    //Create override version depending on the need to account for the ability or situation
    public virtual void OnTriggerEnter(Collider other)
    {
        ITakeDamage t = other.GetComponent<ITakeDamage>();
        if(t != null)
        {
            DealDamage(t, 0);
        }
    }

    public virtual void DealDamage(ITakeDamage target, int amount)
    {
        target.TakeDamage(amount);
    }
}
