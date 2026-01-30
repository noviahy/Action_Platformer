using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    // public 변경 필요
    public string StageID;
    public float? timeLimit;
    
    public List<PlayerSpawnData> Players;
    public List<MonsterSpawnData> Monsters;
    public List<ObstacleSpawnData> Obstacles;
    public List<ItemSpawnData> Items;
 
}
[System.Serializable]
public class PlayerSpawnData // 일관성
{
    public Vector3 position;
}

[System.Serializable]
public class MonsterSpawnData
{
    public string monsterID;
    public List<Vector3> position;
}

[System.Serializable]
public class ObstacleSpawnData
{
    public string obstacleID;
    public List<Vector3> position;
}

[System.Serializable]
public class ItemSpawnData
{
    public string itemID;
    public List<Vector3> position;
}