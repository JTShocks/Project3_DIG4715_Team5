using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public List<Image> abilityImages;
    //ORDER OF THE LIST
    // 0 = Core
    //1 = Hand
    //2 = Leg
    //3 = Weapon

    //Assign each ability a spot
    //Start will NULL
    void OnEnable(){
        AbilitiesManager.OnEquipAbility += EnableUISlot;
    }

    void OnDisable(){
        AbilitiesManager.OnEquipAbility -= EnableUISlot;
    }

    void EnableUISlot(Ability ability)
    {
        //Check the ability, get the image
        //For each image in the list, assign it based on the ability slot first

        switch(ability.abilitySlot)
        {
            case AbilitySlot.Core:
            abilityImages[0].sprite = ability.abilitySprite;
            break;
            case AbilitySlot.Hand:
            abilityImages[1].sprite = ability.abilitySprite;
            break;
            case AbilitySlot.Leg:
            abilityImages[2].sprite = ability.abilitySprite;
            break;
            case AbilitySlot.Weapon:
            abilityImages[3].sprite = ability.abilitySprite;
            break;
        }
    }
}
