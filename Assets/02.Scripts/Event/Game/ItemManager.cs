using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemPrefabPair> ItemPrefabList;
    private Dictionary<string, GameObject> ItemPrefabDict;

    private void Awake()
    {
        ItemPrefabDict = new Dictionary<string, GameObject>();

        foreach (var pair in ItemPrefabList)
        {
            if (!ItemPrefabDict.ContainsKey(pair.itemID))
            {
                ItemPrefabDict.Add(pair.itemID, pair.prefab);
            }
            else
            {
                Debug.LogWarning($"Duplicate Item ID: {pair.itemID}");
            }
        }
    }
    public void SpawnItem(List<ItemSpawnData> ItemList)
    {
        foreach (var data in ItemList)
        {
            if (!ItemPrefabDict.TryGetValue(data.itemID, out var prefab))
            {
                Debug.LogError($"Item ID not registered: {data.itemID}");
            }

            foreach (var pos in data.position)
            {
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}
[System.Serializable]
public class ItemPrefabPair
{
    public string itemID;
    public GameObject prefab;
}
