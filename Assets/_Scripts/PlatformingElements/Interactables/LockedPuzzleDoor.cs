using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedPuzzleDoor : MonoBehaviour
{
    public static LockedPuzzleDoor Instance;
    public PuzzleButton[] buttons;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckButtons()
    {
        foreach (PuzzleButton button in buttons)
        {
            if (!button.isActive)
                return;
        }
        OpenDoor();


        void OpenDoor()
        {
            Destroy(gameObject);
        }
    }
}
