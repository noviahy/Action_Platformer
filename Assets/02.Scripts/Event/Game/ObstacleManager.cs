using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private List<ObstaclePrefabPair> obstaclePrefabList;
    private Dictionary<string, GameObject> obstaclePrefabDict;

    private void Awake()
    {
        obstaclePrefabDict = new Dictionary<string, GameObject>();

        foreach (var pair in obstaclePrefabList)
        {
            if (!obstaclePrefabDict.ContainsKey(pair.obstacleID))
            {
                obstaclePrefabDict.Add(pair.obstacleID, pair.prefab);
            }
            else
            {
                Debug.LogWarning($"Duplicate Obstacle ID: {pair.obstacleID}");
            }
        }
    }
    public void SpawnObstacle(List<ObstacleSpawnData> obstacleList)
    {
        foreach (var data in obstacleList)
        {
            if (!obstaclePrefabDict.TryGetValue(data.obstacleID, out var prefab))
            {
                Debug.LogError($"Obstacle ID not registered: {data.obstacleID}");
            }

            foreach (var pos in data.position)
            {
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}
[System.Serializable]
public class ObstaclePrefabPair
{
    public string obstacleID;
    public GameObject prefab;
}