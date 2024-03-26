using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    void OnEnable(){GameManager.OnGamePause += ActivateMenuUI;}
    void OnDisable(){GameManager.OnGamePause -= ActivateMenuUI;}

    //When the game is paused, show the UI for pausing

    [SerializeField] GameObject testUI;

    void ActivateMenuUI(bool isGamePaused)
    {
        testUI.SetActive(isGamePaused);
    }
    public void EquipAbility(Ability ability)
    {
        AbilitiesManager.instance.EquipAbility(ability);
    }
}
