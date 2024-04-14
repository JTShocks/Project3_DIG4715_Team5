using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public bool isActive = false;
    public GameObject indicatorLight;

    void OnTriggerEnter(Collider other)
    {
        if(!isActive && other.CompareTag("Player"))
        {
            isActive = true;
            indicatorLight.GetComponent<Renderer>().material.color = Color.green;
            LockedPuzzleDoor.Instance.CheckButtons();
        }
    }
}
