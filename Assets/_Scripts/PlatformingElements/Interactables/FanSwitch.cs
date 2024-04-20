using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSwitch : MonoBehaviour
{
    public int id;
    [SerializeField] Interactable switchForFans;
    [SerializeField] List<GameObject> fans; // List of fans to turn on or off.
    
    void OnEnable()
    {
        if(switchForFans != null)
        {
            switchForFans.OnInteract += ToggleFans;
        }
    }

    void OnDisable()
    {
        if(switchForFans != null)
        {
            switchForFans.OnInteract -= ToggleFans;
        }
    }
    
    void ToggleFans()
    {
        Debug.LogFormat("Fan Switch {0} activated by event", id);

        foreach(GameObject fan in fans)
        {
            if(fan != null)
            {
                bool isActive = !fan.activeSelf;
                fan.SetActive(isActive);

                Animator animator = fan.GetComponent<Animator>();
                if(animator != null)
                {
                    animator.SetBool("IsOn", isActive);
                }
            }
        }
    }
}
