using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<Transform> respawnStations;
    private int currentCheckpointIndex = 0;

    public void UpdateCheckpointIndex(int newIndex)
    {
        currentCheckpointIndex = newIndex;
        Debug.Log("Checkpoint saved.");
    }

    public void RespawnPlayer(GameObject player)
    {
        if(currentCheckpointIndex < respawnStations.Count)
        {
            Transform respawnPoint = respawnStations[currentCheckpointIndex];
            player.transform.position = respawnPoint.position;
        }
    }
}
