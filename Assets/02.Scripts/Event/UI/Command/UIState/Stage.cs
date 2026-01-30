using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stage : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup stageUI;
    [SerializeField] private TextMeshPro stage1;
    [SerializeField] private TextMeshPro stage2;
    [SerializeField] private TextMeshPro stage3;
    [SerializeField] private TextMeshPro selectedStage;
    [SerializeField] private TextMeshPro bestTime;
    [SerializeField] private TextMeshPro life;
    [SerializeField] private TextMeshPro coin;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Image coinImage;
    public override EStateType StateType => EStateType.Stage;
    public override void Enter()
    {
        uiManager.RefreshStageBT();
        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        stageUI.alpha = value ? 1f : 0f;
        stageUI.interactable = value;
        stageUI.blocksRaycasts = value;
    }
}
