using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<StageData> allStageData;

    public StageData GetStageData(string returnStageID)
    {
        foreach (var data in allStageData)
        {
            if (data.StageID == returnStageID)
            {
                return data;
            }
        }
        Debug.LogError($"Stage Data not found: {returnStageID}");
        return null;
    }
}
