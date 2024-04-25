using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    internal int menuIndex;

    void OnEnable(){
        MenuController.OnMenuSetActive += MenuSetActive;
        MenuController.OnMenuDisable += MenuDisable;
    }

    void OnDisable(){
        MenuController.OnMenuSetActive -= MenuSetActive;
        MenuController.OnMenuDisable -= MenuDisable;
    }

    void Awake()
    {
        menuIndex = transform.GetSiblingIndex();
    }
    

    public virtual void MenuSetActive()
    {
        
    }

    public virtual void MenuDisable()
    {

    }
}
