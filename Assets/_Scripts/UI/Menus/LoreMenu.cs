using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LoreMenu : GameMenu
{
    //On the left, the buttons for each UI element.
    //On the right, the text

    [SerializeField] TextMeshProUGUI loreTitle;
    [SerializeField] TextMeshProUGUI loreDescription;

    [SerializeField] GameObject loreButtons;

    List<LoreButton> theButtons = new();




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

    public override void MenuSetActive(int menu)
    {
        base.MenuSetActive(menu);
        int childCount = loreButtons.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            LoreButton nextButton = loreButtons.transform.GetChild(i).GetComponent<LoreButton>();
            theButtons.Add(nextButton);

            try
            {
                nextButton.EnableLoreButton(LoreManager.sortedLore[i]);
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.Log("No lore to show. Button should not work");
            }
        }

        theButtons[0].button.Select();



    }


    //Make a class LoreButton that gets it's sibling index and passes it through to this menu to display the correct lore for that index


    public void DisplayLore(int index)
    {
        loreTitle.text = LoreManager.sortedLore[index].LoreTitle;
        loreDescription.text = LoreManager.sortedLore[index].LoreText;
    }


}
