using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
   //This script creates and controls how many of the lightbulbs are showing on the screen
   //It is only in change of instantiating the assets

   //It will work by creating the list of children, starting at the parent origin, then each child will be moved over X space by the current child position.

   [SerializeField] List<Image> healthImages = new();
   [SerializeField] Sprite bulbOn;
   [SerializeField] Sprite bulbOff;


    void OnEnable(){GameManager.OnGameLoad += PlaceHealthImages;
    PlayerHealth.OnPlayerChangeHealth += PlaceHealthImages;}
    void OnDisable(){GameManager.OnGameLoad -= PlaceHealthImages;
    PlayerHealth.OnPlayerChangeHealth -= PlaceHealthImages;
    }

   void PlaceHealthImages()
   {

        for(int i =0; i < PlayerHealth.MaxHealth; i++)
        {

            if(i < PlayerHealth.CurrentHealth)
            {
                healthImages[i].sprite = bulbOn;
            }
            else{
                healthImages[i].sprite = bulbOff;
            }

            if(i < PlayerHealth.MaxHealth)
            {
                healthImages[i].enabled = true;
            }
            else{
                healthImages[i].enabled = false;
            }
            //Activate each heart based on how much health the player has at the moment
        }
   }

}
