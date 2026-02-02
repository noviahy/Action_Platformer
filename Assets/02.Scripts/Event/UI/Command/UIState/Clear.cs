using UnityEngine;

public class Clear : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup clearUI;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private StageProgressManager stageProgressManager;
    public override EStateType StateType => EStateType.Clear;
    public override void Enter()
    {
        stageProgressManager.SetCleared($"{uiManager.SelectedWorld}-{uiManager.SelectedStage}", true, timeManager.Timer);
        setVisible(true);
    }
    public override void Exit()
    {
        uiManager.RequestEvent();
        uiManager.RefreshStageBT();
        EventManager.OnGameOverUI -= () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI -= () => uiManager.ChangeState(EStateType.Clear);
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        clearUI.alpha = value ? 1f : 0f;
        clearUI.interactable = value;
        clearUI.blocksRaycasts = value;
    }
}
