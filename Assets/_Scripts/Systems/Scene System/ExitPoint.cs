using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public int index;
    public string sceneToLoad;
    public Transform Exit;
    public FadeScreen fadeScreen;

    // Set the scene to load upon the player entering the trigger.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.entranceToUse = index;

            fadeScreen.ActivateFadeIn();

            Invoke("LoadScene", 0.4f);
        }
    }

    private void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
