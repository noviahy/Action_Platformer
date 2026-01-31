using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class StageWindow : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CoinHPCalculator coinHPCalculator;
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
    public void SetStageText()
    {
        stage1.text = uiManager.SelectedWorld + "-1";
        stage2.text = uiManager.SelectedWorld + "-2";
        stage3.text = uiManager.SelectedWorld + "-3";
        life.text = $"{coinHPCalculator.HP}";
        coin.text = $"{coinHPCalculator.Coin}";
    }

    public void setDefaultWindowData()
    {
        List<StageProgressData> stageList = uiManager.ReQuestStageDataList();

        for (int i = stageList.Count; i <= 0; i--)
        {
            if (stageList[i].isOpened)
            {
                string[] parts = stageList[i].StageID.Split('-');
                uiManager.SetStageNum(parts[1]);
                SetWindowData();
                return;
            }
        }
    }

    public void SetWindowData()
    {
        StageProgressData stageData = uiManager.RequestStageData();
        selectedStage.text = uiManager.SelectedWorld + uiManager.SelectedStage;
        bestTime.text = $"{stageData.BestTime}";
    }
}
