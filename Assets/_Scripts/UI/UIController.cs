using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    void OnEnable(){GameManager.OnGamePause += ActivateMenuUI;}
    void OnDisable(){GameManager.OnGamePause -= ActivateMenuUI;}

    //When the game is paused, show the UI for pausing

    [SerializeField] GameObject testUI;

    //Make the UI into a prefab object that is instantiated by the game manager
    //Once it is made, activate the different screens
    //If we do it right, the UI should be controlled easily and not conflict with the player (we should change to the UI controls)

    void ActivateMenuUI(bool isGamePaused)
    {
        testUI.SetActive(isGamePaused);
    }
    public void EquipAbility(Ability ability)
    {
        AbilitiesManager.instance.EquipAbility(ability);
    }

    //Get the player actions reference, use that to equip abilities based on the slot for each ability

    //Each ability by category, assigned to a direction to quick equip
    //Have on pause the menus, use RB and LB to switch which menu is active
    //Pause/ escape always closes the menu and is a way to get out of ANY menu
}
