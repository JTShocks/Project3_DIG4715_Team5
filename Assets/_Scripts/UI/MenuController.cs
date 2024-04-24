using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
   
   PlayerInput input;
    InputAction changeMenuAction;

    GameObject activeMenu;
    //This gets the children and stores the active child
    //The Animator slides between them, activating them and moving them around.

    float timer;
    float timeBetweenMenus = .5f;


    //Each are animated from their PARENT, which has nothing OTHER than being enabled or not and being moved around the screen

    //3 menus

    //Default pause menu
        //Continue
        //Controls
        //To Main Menu
        //Quit Game
            //Note: Add a "are you sure?" message for quitting the game"

    //Lore Menu
        //On the left side, a bunch of buttons that, on hover, shows the lore on the right side
        //Player should be able to just use the left stick to move up and down the list

    //Abilities menu
        //Oriented around the center design
        //Reflected of the ability menu in game.
            //Is a way to find the reminder text for each ability

        //When an ability is selected, those buttons open the display for all the abilities, which are a series of buttons
        //When the button is pressed, the ability is equipped and the image display is that of the active ability for the given slot

    void OnEnable(){



    }
    void OnDisable(){

    }

    void Awake()
    {
        changeMenuAction = PlayerController.playerInput.actions["changemenu"];
        activeMenu = transform.GetChild(0).gameObject;
        activeMenu.SetActive(true);
        activeMenu.GetComponentInChildren<Button>().Select();
    }
    void Update()
    {
        var input  = changeMenuAction.ReadValue<Vector2>();
        input.Normalize();

        timer -= Time.unscaledDeltaTime;

        if(input.x != 0 && gameObject.activeInHierarchy && timer < 0)
        {
            SwitchMenus(input.x);
            timer = timeBetweenMenus;
        }
    }


    void SwitchMenus(float direction)
    {

        GameObject previousMenu = activeMenu;
        Transform nextMenu = null;
        //This is the function for actually moving the menus and is triggered by the player input

        switch(direction)
        {
            case -1:
            //Left
            nextMenu = GetPreviousMenu(activeMenu.transform);
            break;
            case 1:
            //Right
            nextMenu = GetNextMenu(activeMenu.transform);
            break;
        }
        previousMenu.SetActive(false);
        nextMenu.gameObject.SetActive(true);
        activeMenu = nextMenu.gameObject;

        //Direction is the X value fo the Vector for the RB and LB buttons. That is passed through as a way to increment the index to the active child

        
        
    }

    public Transform GetNextMenu(Transform currentMenu)
    {
        if(currentMenu == null)
        {
            //If there is not a valid point, default to the starting waypoint
            return transform.GetChild(0);
        }

        if(currentMenu.GetSiblingIndex() < transform.childCount -1)
        {
            //If there are still waypoints in the object, get the next one in series through the siblings
            return transform.GetChild(currentMenu.GetSiblingIndex()+1);  
        }
        else
        {
            //If all else fails, default to the first waypoint in the index
            return transform.GetChild(0);
        }
    }
        public Transform GetPreviousMenu(Transform currentMenu)
    {
        if(currentMenu == null)
        {
            //If there is not a valid point, default to the starting waypoint
            return transform.GetChild(0);
        }

        if(currentMenu.GetSiblingIndex() > 0)
        {
            //If there are still waypoints in the object, get the next one in series through the siblings
            return transform.GetChild(currentMenu.GetSiblingIndex()-1);  
        }
        else
        {
            //If all else fails, default to the first waypoint in the index
            return transform.GetChild(transform.childCount-1);
        }
    }


}
