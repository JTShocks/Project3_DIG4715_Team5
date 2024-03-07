using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public int index;
    public string sceneToLoad;
    public Transform Exit;

    // Set the scene to load upon the player entering the trigger.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.entranceToUse = index;

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }
}
