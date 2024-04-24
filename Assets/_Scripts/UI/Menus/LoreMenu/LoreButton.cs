using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;
using UnityEngine.UI;



public class LoreButton : MonoBehaviour
{

    public int index; 
    public Button button;
    TextMeshProUGUI buttonText;

    public static event Action<int> OnLoreButtonClick;


    void OnEnable(){
        LoreManager.OnPickupLore += EnableLoreButton;


    }
    void OnDisable(){
        LoreManager.OnPickupLore -= EnableLoreButton;
    }

    void Awake()
    {
        index = transform.GetSiblingIndex();
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button.interactable = false;
    }

    public void EnableLoreButton(Lore lore)
    {

        if(lore == null)
        {
            throw new ArgumentOutOfRangeException();
        }
        if(lore.IndexValue == index)
        {
            button.interactable = true;
            buttonText.text = lore.LoreTitle;

        }
    }

    public void ShowLore()
    {
        OnLoreButtonClick?.Invoke(index);
    }
}