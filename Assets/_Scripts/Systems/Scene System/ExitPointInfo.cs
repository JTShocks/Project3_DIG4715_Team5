using UnityEngine;

[System.Serializable]
public class ExitPointInfo
{
    public int index;
    public string sceneToLoad;
    public Transform Exit;

    public ExitPointInfo(int index, string sceneToLoad, Transform Exit)
    {
        this.index = index;
        this.sceneToLoad = sceneToLoad;
        this.Exit = Exit;
    }
}
