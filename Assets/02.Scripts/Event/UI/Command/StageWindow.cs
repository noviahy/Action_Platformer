using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml.Schema;
public class StageWindow : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CoinHPCalculator coinHPCalculator;
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
    public void setDefaultWindowData()
    {
        stage1.text = uiManager.SelectedWorld + "-1";
        stage2.text = uiManager.SelectedWorld + "-2";
        stage3.text = uiManager.SelectedWorld + "-3";
        life.text = $"X {coinHPCalculator.HP}";
        coin.text = $"X {coinHPCalculator.Coin}";

        if (uiManager.previousState.StateType == EStateType.Pause)
        {
            
            SetWindowData();
            return;
        }

        List<StageProgressData> stageList = uiManager.ReQuestStageDataList();

        for (int i = stageList.Count - 1; i > 0; i--)
        {
            if (stageList[i].isOpened)
            {
                string[] parts = stageList[i].StageID.Split('-');
                uiManager.SetStageNum(parts[1]);
                SetWindowData();
                return;
            }
        }

        uiManager.SetStageNum("1");
        SetWindowData();
    }
    public void SetWindowData()
    {
        StageProgressData stageData = uiManager.RequestStageData();

        selectedStage.text = uiManager.SelectedWorld + "-" + uiManager.SelectedStage;

        float totalSeconds = stageData.BestTime;

        int minutes = (int)(totalSeconds / 60);
        int seconds = (int)(totalSeconds % 60);
        int milliseconds = (int)((totalSeconds - (int)totalSeconds) * 1000);

        bestTime.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";

        // startButton 활성화 여부
        startButton.interactable = stageData.isOpened;

    }
}
