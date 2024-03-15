using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{


    public int id;
    [SerializeField] Interactable switchForDoor;
    // Start is called before the first frame update
    void OnEnable(){
        if(switchForDoor != null)
        {
            switchForDoor.OnInteract += ActivateDoor;
        }

    }

    void OnDisable(){
           if(switchForDoor != null)
        {
            switchForDoor.OnInteract -= ActivateDoor;
        }
    }

    void ActivateDoor()
    {
        Debug.LogFormat("Door {0} activated by event", id);
    }
}
