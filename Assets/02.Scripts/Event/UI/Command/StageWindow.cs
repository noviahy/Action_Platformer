using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// UIState.Stage, StageButton
public class StageWindow : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text stage1;
    [SerializeField] private TMP_Text stage2;
    [SerializeField] private TMP_Text stage3;
    [SerializeField] private TMP_Text selectedStage;
    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text coin;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Image coinImage;
    [SerializeField] private Image collectionCoin;

    public void setDefaultWindowData() // UIState.Stage
    {
        stage1.text = uiManager.SelectedWorld + "-1";
        stage2.text = uiManager.SelectedWorld + "-2";
        stage3.text = uiManager.SelectedWorld + "-3";
        life.text = $"X {uiManager.RequestHP()}";
        coin.text = $"X {uiManager.RequestCoin()}";

        if (uiManager.previousState.StateType == EStateType.Pause) // Already have a seleted stage
        {
            SetWindowData();
            return;
        }

        List<StageProgressData> stageList = uiManager.RequestStageDataList(); // Get WorldData List from UIManager

        for (int i = stageList.Count - 1; i > 0; i--) // Outputs the highest unlocked stage
        {
            if (stageList[i].isOpened)
            {
                string[] parts = stageList[i].StageID.Split('-'); // abandon world ID
                uiManager.SetStageNum(parts[1]); // Get stage ID
                SetWindowData();
                return;
            }
        }

        uiManager.SetStageNum("1");
        SetWindowData();
    }
    public void SetWindowData() // UIButton (When World Button Clicked)
    { 
        // Get stage data form UIManager (using UIManager's SelectedWorld SelectedStage)
        StageProgressData stageData = uiManager.RequestStageData();

        selectedStage.text = stageData.StageID; // Set stageWindow StageID

        float totalSeconds = stageData.BestTime; 

        int minutes = (int)(totalSeconds / 60);
        int seconds = (int)(totalSeconds % 60);
        int milliseconds = (int)((totalSeconds - (int)totalSeconds) * 1000);

        bestTime.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";

        
        startButton.interactable = stageData.isOpened; // Decide startButton activate

    }
}
