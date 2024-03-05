using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance {get; private set;}

    [Header("Player Unlocked Abilities")]

    public List<Ability> unlockedAbilities;

    //In this list, the abilities that are unlocked are stored.
    //The ability controller will double check this list


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    //What is the abilities manager?
    //This is where the abilities are going to be controlled.
    //If the ability is disabled, you cannot select it to be equipped.

    //How does this work?
    // Will work be adding and removing components from the Player controller
    // When the scene is loaded, this will make sure the player controller object has the required components active on it
    //Will instantiate an AbilityHolder for the ability and place the ability data in the given slot
    //When the ability is unequipped, it will deactivate the component on the player

    //The AbilitiesController (located on the player) is what activates and deactivates the components for each ability
}
