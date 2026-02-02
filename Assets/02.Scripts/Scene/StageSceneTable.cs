using System.Collections.Generic;
using UnityEngine;

// ChangeSceneManager
public static class StageSceneTable
{
    private static readonly Dictionary<string, string> stageToScene = new Dictionary<string, string>
    {
        {"UIScene", "UIScene"},
        {"1-1", "Stage_1_1"},
        {"1-2", "Stage_1_2"},
        {"1-3", "Stage_1_3"},
        {"2-1", "Stage_2_1"},
        {"2-2", "Stage_2_2"},
        {"2-3", "Stage_2_3"},
        {"3-1", "Stage_3_1"},
        {"3-2", "Stage_3_2"},
        {"3-3", "Stage_3_3"}
    };

    public static string GetSceneName(string stageID) // ChangeSceneManager
    {
        if(!stageToScene.TryGetValue(stageID, out var scene))
        {
            Debug.LogError($"Scene not found StageID: {stageID}");
            return string.Empty;
        }
        return scene;
    }
}
