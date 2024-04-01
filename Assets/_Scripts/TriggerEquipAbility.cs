using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEquipAbility : MonoBehaviour
{


    [SerializeField] Ability ability;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            AbilitiesManager.instance.EquipAbility(ability);
        }
    }
}
