using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject currentPlayer;
    public void SpawnPlayer(List<PlayerSpawnData> playerList)
    {
        if (playerList == null)
        {
            Debug.LogWarning("PlayerSpawnData is empty");
        }
        var data = playerList[0];

        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        currentPlayer = Instantiate(playerPrefab, data.position, Quaternion.identity);
    }
}
