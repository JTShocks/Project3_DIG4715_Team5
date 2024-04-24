using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreManager : MonoBehaviour
{
    //This is a static class that holds all the collected lore
     public static LoreManager instance;
    public static event Action<Lore> OnPickupLore;
    public static List<Lore> collectedLore = new();

    public static List<Lore> sortedLore = new();


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
            OnPickupLore?.Invoke(lore);
            SortLore();


        }
    }

    /// <summary>
    /// Sort the list of the lore to make sure it displays in the proper order according to the order of the buttons in the menu
    /// </summary>
    void SortLore()
    {
        for(int i = 0; i < collectedLore.Count; i++)
        {
            foreach(Lore lore in collectedLore)
            {
                if(lore.IndexValue == i)
                {
                    sortedLore.Insert(lore.IndexValue, lore);
                }
                else
                {
                    Lore blankLore = ScriptableObject.CreateInstance<Lore>();
                    sortedLore.Insert(i, blankLore);
                }
            }
        }
    }

    
}
