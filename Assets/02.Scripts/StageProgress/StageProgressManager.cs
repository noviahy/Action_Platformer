using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// UIManager, GameManager, CoinHPManager
// Send Data to SaveLoadManager
public class StageProgressManager : MonoBehaviour
{
    // 기본값 템플릿
    [SerializeField] private List<StageProgressData> stageTemplates;

    // 실제 게임 중에 쓰는 데이터
    private Dictionary<string, StageProgressData> stageDataDict;
    private void Awake()
    {
        Initialize();
    }

    // 초기화: 저장된 데이터 불러오거나 새로 생성
    public void Initialize()
    {
        stageDataDict = new Dictionary<string, StageProgressData>();

        // JSON 파일에서 이전에 저장된 데이터들
        // 처음 실행이면 null
        StageProgressData[] savedData = SaveLoadManager.Load();

        // 템플릿 기준으로 하나씩 처리
        foreach (var template in stageTemplates)
        {
            StageProgressData data = null;

            if (savedData != null)
            {
                foreach (var saved in savedData)
                {
                    if (saved.StageID == template.StageID)
                    {
                        data = saved;
                        break;
                    }
                }
            }

            // 없을 경우: 템플릿 복사
            // 에디터용 템플릿 -> 런타임용 데이터 복제
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<StageProgressData>();
                data.StageID = template.StageID;
                data.isCleared = template.isCleared;
                data.isOpened = template.isOpened;
                data.BestTime = template.BestTime;
                data.CollectedCoin = template.CollectedCoin;
            }

            // Dictionary에 등록
            stageDataDict[template.StageID] = data;
        }
    }
    // Return World Data
    public List<StageProgressData> GetWorldData(string worldID) // UIManager
    {
        List<StageProgressData> stageList = stageDataDict.Values
     .Where(stage => stage.StageID.Split('-')[0] == worldID)
     .ToList();
        return stageList;
    }
    // Return Stage Data
    public StageProgressData GetStageData(string stageID) // UIManager
    {
        stageDataDict.TryGetValue(stageID, out var data);
        return data;
    }

    // Set Stage Data and open next stage
    public void SetCleared(string stageID, bool cleared, float time) // GameManager (GameState.Clear)
    {
        if (stageDataDict.TryGetValue(stageID, out var data))
        {
            setBestTime(stageID, time);
            data.isCleared = cleared;
            string nextStage = getNextStageID(stageID);
            if (!string.IsNullOrEmpty(nextStage))
            {
                SetOpened(nextStage, true);
            }
        }
    }
    // when get CollectionCoin
    public void SetCollectedCoin(string stageID, bool collected) // CollectedCoin
    {
        if (stageDataDict.TryGetValue(stageID, out var data))
        {
            data.CollectedCoin = collected;
        }
    }
    // Save All Stage Data
    public void SaveAll() // GameManager (GameState.Clear)
    {
        SaveLoadManager.Save(new List<StageProgressData>(stageDataDict.Values).ToArray());
    }

    // private
    private void setBestTime(string stageID, float time)
    {
        if (stageDataDict.TryGetValue(stageID, out var data))
        {
            if (data.BestTime <= 0 || time < data.BestTime)
            {
                data.BestTime = time;
            }
        }
    }
    private void SetOpened(string stageID, bool opened)
    {
        if (stageDataDict.TryGetValue(stageID, out var data))
        {
            data.isOpened = opened;
        }
    }
    private string getNextStageID(string currentStageID)
    {
        for (int i = 0; i < stageTemplates.Count - 1; i++)
        {
            if (stageTemplates[i].StageID == currentStageID)
            {
                return stageTemplates[i + 1].StageID;
            }
        }
        return null;
    }
}