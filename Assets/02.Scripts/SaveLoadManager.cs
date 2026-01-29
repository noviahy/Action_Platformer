using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    private static string path => Application.persistentDataPath + "/stageProgress.json";

    [System.Serializable]
    private class StageProgressListWrapper
    {
        // Unity의 JsonUtility는 배열 단독 저장 불가
        // JsonUtility.ToJson(StageProgressSaveData[]) 불가능
        // 배열을 담기 위한 박스 생성
        public StageProgressSaveData[] stages;
    }

    // 저장
    public static void Save(StageProgressData[] allData)
    {
        // ScriptableObject → SaveData로 변환할 변수
        StageProgressSaveData[] saveData = new StageProgressSaveData[allData.Length];
        for (int i = 0; i < allData.Length; i++)
        {
            var d = allData[i];
            saveData[i] = new StageProgressSaveData
            {
                StageID = d.StageID,
                isCleared = d.isCleared,
                isOpened = d.isOpened,
                BestTime = d.BestTime,
                CollectedCoin = d.CollectedCoin
            };
        }
        // saveData로 변환 완료

        // ScriptableObject 안의 값만 복사
        string json = JsonUtility.ToJson(new StageProgressListWrapper { stages = saveData }, true);
        File.WriteAllText(path, json);
        // JSON으로 저장 완료
    }

    public static StageProgressData[] Load()
    {
        // 파일이 있는지 확인
        if (!File.Exists(path)) return null;

        // 문자열로 읽음
        string json = File.ReadAllText(path);
        // JSON -> Wrapper 객체 변환
        var wrapper = JsonUtility.FromJson<StageProgressListWrapper>(json);

        // SaveData → StageProgressData(ScriptableObject)로 변환
        StageProgressData[] runtimeData = new StageProgressData[wrapper.stages.Length];
        for (int i = 0; i < wrapper.stages.Length; i++)
        {
            var s = wrapper.stages[i];
            var d = ScriptableObject.CreateInstance<StageProgressData>();
            d.StageID = s.StageID;
            d.isCleared = s.isCleared;
            d.isOpened = s.isOpened;
            d.BestTime = s.BestTime;
            d.CollectedCoin = s.CollectedCoin;
            runtimeData[i] = d;
        }

        return runtimeData;
    }
}
// JSON으로 저장하기 위한 순수 C# 데이터 묶음
[System.Serializable]
public class StageProgressSaveData
{
    public string StageID;
    public bool isCleared;
    public bool isOpened;
    public float BestTime;
    public bool CollectedCoin;
}