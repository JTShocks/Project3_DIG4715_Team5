using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent)
    {

    }
}
