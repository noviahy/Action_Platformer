using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private ObstacleManager obstacleManager;
    [SerializeField] private ItemManager itemManager;
    public void SpawnStage(string stageID)
    {
        StageData stageData = stageManager.GetStageData(stageID);
        playerManager.SpawnPlayer(stageData.Players);
        monsterManager.SpawnMonster(stageData.Monsters);
        obstacleManager.SpawnObstacle(stageData.Obstacles);
        itemManager.SpawnItem(stageData.Items);
        
    }
       
}
