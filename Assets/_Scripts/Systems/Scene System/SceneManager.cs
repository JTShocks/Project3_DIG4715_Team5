using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static int entranceToUse;
    public List<ExitPointInfo> exitPoints;
    public FadeScreen fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerPosition();

        fadeScreen.ActivateFadeOut();
    }

    public void SetPlayerPosition()
    {
        // Find the entrance to use.
        ExitPointInfo entrance = exitPoints.Find(x => x.index == entranceToUse);
        if (entrance != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Place the player.
            CharacterController controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
            player.transform.position = entrance.Exit.position;
            controller.enabled = true;
        }
    }
}