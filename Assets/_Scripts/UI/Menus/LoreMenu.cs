using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoreMenu : MonoBehaviour
{
    //On the left, the buttons for each UI element.
    //On the right, the text

    [SerializeField] TextMeshProUGUI loreTitle;
    [SerializeField] TextMeshProUGUI loreDescription;

    //Each button will have a Lore assigned to it, based on their sibling index

    



    public void DisplayLore(Lore lore)
    {
        loreTitle.text = lore.LoreTitle;
        loreDescription.text = lore.LoreText;
    }
}
