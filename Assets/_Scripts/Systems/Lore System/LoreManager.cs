using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreManager : MonoBehaviour
{
    //This is a static class that holds all the collected lore
     public static LoreManager instance;
    public static event Action OnPickupLore;
    public static List<Lore> collectedLore = new();


    void OnEnable(){

    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public void CollectLore(Lore lore)
    {
        if(!collectedLore.Contains(lore))
        {
            collectedLore.Add(lore);
            OnPickupLore?.Invoke();
        }
    }

    
}
