using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform respawnPoint;
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        RespawnManager respawnManager = FindObjectOfType<RespawnManager>();
        if(respawnManager != null)
        {
            respawnManager.RespawnPlayer(player);
        }
        else
        {
            UnityEngine.Debug.LogError("RespawnManager not found in the scene!");
        }
    }
}
