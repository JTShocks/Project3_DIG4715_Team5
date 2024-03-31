using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMessage : MonoBehaviour
{
    public Text winText; 

    private void Start()
    {
        // Ensure the win text is initially hidden
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            winText.text = "You Win!";
            winText.gameObject.SetActive(true);
        }
    }
}