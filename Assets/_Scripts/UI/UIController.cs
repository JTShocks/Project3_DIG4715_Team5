using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    

    public void EquipAbility(Ability ability)
    {
        AbilitiesManager.instance.EquipAbility(ability);
    }
}
