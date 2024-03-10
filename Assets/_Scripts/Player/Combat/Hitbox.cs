using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        ITakeDamage t = other.GetComponent<ITakeDamage>();
        if(t != null)
        {
            t.TakeDamage(0);
        }
    }
}
