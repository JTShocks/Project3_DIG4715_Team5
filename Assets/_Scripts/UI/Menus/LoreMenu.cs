using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LoreMenu : MonoBehaviour
{
    //On the left, the buttons for each UI element.
    //On the right, the text

    [SerializeField] TextMeshProUGUI loreTitle;
    [SerializeField] TextMeshProUGUI loreDescription;




    //Each button will have a Lore assigned to it, based on their sibling index

    void OnEnable(){
        LoreButton.OnLoreButtonClick += DisplayLore;
    }

    void OnDisable(){
        LoreButton.OnLoreButtonClick -= DisplayLore;
    }

    void Awake()
    {
        //Need to find a way to make the buttons behave consistently
    }


    //Make a class LoreButton that gets it's sibling index and passes it through to this menu to display the correct lore for that index


    public void DisplayLore(int index)
    {
        loreTitle.text = LoreManager.sortedLore[index].LoreTitle;
        loreDescription.text = LoreManager.sortedLore[index].LoreText;
    }


}
