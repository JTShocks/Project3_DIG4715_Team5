using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePickup : Pickup
{
    [SerializeField] Lore lore;

    public override void OnPickup()
    {
        //T
        LoreManager.instance.CollectLore(lore);
        base.OnPickup();
    }
}
