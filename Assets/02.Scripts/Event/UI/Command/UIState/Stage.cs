using UnityEngine;

public class Stage : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private CanvasGroup stageUI;
    [SerializeField] private StageWindow stageWindow;
    public override EStateType StateType => EStateType.Stage;
    public override void Enter()
    {
        if (uiManager.previousState.StateType == EStateType.Pause)
        {
            eventManager.RequestChangeScene();
        }
        uiManager.RefreshStageBT();
        stageWindow.setDefaultWindowData();
        
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
