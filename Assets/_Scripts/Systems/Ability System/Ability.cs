using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Ability : ScriptableObject
{
    public enum AbilitySlot{
        Core,
        Hand,
        Leg,
        Weapon
    }
    public new string name;
    [TextArea]
    public string description;
    public float cooldownTime;
    public float activeTime;
    public AbilitySlot abilitySlot;

    public virtual void Activate(GameObject parent)
    {

    }
    public virtual void Deactivate(GameObject parent)
    {
        
    }
}
