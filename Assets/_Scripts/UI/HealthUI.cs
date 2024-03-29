using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
   //This script creates and controls how many of the lightbulbs are showing on the screen
   //It is only in change of instantiating the assets

   //It will work by creating the list of children, starting at the parent origin, then each child will be moved over X space by the current child position.

   [SerializeField] Image litBulb;
   [SerializeField] Image darkBulb;

   List<Image> healthImages = new();

   void PlaceHealthImages()
   {
        float bulbDistanceDiff = 10;

        for(int i =0; i < PlayerHealth.MaxHealth; i++)
        {
            //Place in an image for each health position, using the previous child as a reference
            Image newBulb = Instantiate(litBulb, this.transform);
            newBulb.transform.position = Vector3.right * bulbDistanceDiff;
            healthImages.Add(newBulb);
            
        }
   }

   void LoseHealthStage()
   {
        //Get the diff between maxHealth and currentHealth, use that to loop through all the images and change them into the new image
   }

   void UpdateHealthImages()
   {
        //Start at the image at the end of the list
        //Change the image of that point, related to the missing health.

   }
}
