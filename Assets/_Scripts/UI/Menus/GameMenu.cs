using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    internal int menuIndex;

    void OnEnable(){
        MenuController.OnMenuSetActive += MenuSetActive;
    }

    void OnDisable(){
        MenuController.OnMenuSetActive -= MenuSetActive;
    }

    void Awake()
    {
        menuIndex = transform.GetSiblingIndex();
    }
    

    public virtual void MenuSetActive(int menu)
    {
        
    }
}
