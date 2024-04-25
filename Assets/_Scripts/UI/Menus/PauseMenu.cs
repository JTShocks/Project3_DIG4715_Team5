using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : GameMenu
{

    [SerializeField] Button startButton;
    [SerializeField] Button pauseButton;
    void Awake()
    {
        
    }
    public override void MenuSetActive()
    {
        base.MenuSetActive();
        startButton.Select();
        
    }
}
