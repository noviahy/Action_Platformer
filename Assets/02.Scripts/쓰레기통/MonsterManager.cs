using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private List<MonsterPrefabPair> monsterPrefabList;
    private Dictionary<string, GameObject> monsterPrefabDict;

    private void Awake()
    {
        monsterPrefabDict = new Dictionary<string, GameObject>();

        foreach (var pair in monsterPrefabList)
        {
            if (!monsterPrefabDict.ContainsKey(pair.monsterID))
            {
                monsterPrefabDict.Add(pair.monsterID, pair.prefab);
            }
            else
            {
                Debug.LogWarning($"Duplicate Monster ID: {pair.monsterID}");
            }
        }
    }
    public void SpawnMonster(List<MonsterSpawnData> monsterList)
    {
        foreach (var data in monsterList)
        {
            if (!monsterPrefabDict.TryGetValue(data.monsterID, out var prefab))
            {
                Debug.LogError($"Monster ID not registered: {data.monsterID}");
            }

            foreach (var pos in data.position)
            {
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}
[System.Serializable]
public class MonsterPrefabPair
{
    public string monsterID;
    public GameObject prefab;
}