using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBulb : MonoBehaviour
{

    //Make these icons hold the on and off states
    //Have the health UI call them to turn on and off

    //

    [SerializeField] Sprite bulbOn;
    [SerializeField] Sprite bulbOff;
    public Image currentImage;

    public bool bulbIsOn;

    void Awake()
    {
        currentImage = GetComponent<Image>();
        currentImage.sprite = bulbOn;
        bulbIsOn = true;
    }

    public void SwitchHealthBulb(bool state)
    {
        bulbIsOn = state;
        if(bulbIsOn)
        {
            currentImage.sprite = bulbOn;
        }
        else
        {
            currentImage.sprite = bulbOff;
        }
    }
}
