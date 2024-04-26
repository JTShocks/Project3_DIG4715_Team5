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
    //TextMeshProUGUI buttonText;

    [SerializeField] Image cardImage;

    [SerializeField] AudioClip loreHighlight;
    [SerializeField] AudioClip loreSelect;

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
       // buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button.interactable = false;
    }

    public void EnableLoreButton(Lore lore)
    {

        if(lore == null)
        {
            cardImage.enabled = false;
            throw new ArgumentOutOfRangeException();
        }
        if(lore.IndexValue == index)
        {
            button.interactable = true;
            cardImage.enabled = true;
            //buttonText.text = lore.LoreTitle;

        }
    }

    public void PlayMenuSFX()
    {
        AudioManager.instance.PlaySound(loreHighlight);
    }

    public void ShowLore()
    {
        OnLoreButtonClick?.Invoke(index);
        AudioManager.instance.PlaySound(loreSelect);
    }

}
