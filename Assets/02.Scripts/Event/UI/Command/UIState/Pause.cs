using UnityEngine;

public class Pause : UIState
{
    [SerializeField] private CanvasGroup pauseUI;
    [SerializeField] private UIManager uiManager;
    public override EStateType StateType => EStateType.Pause;
    public override void Enter()
    {
        uiManager.RequestEvent();
        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        pauseUI.alpha = value ? 1f : 0f;
        pauseUI.interactable = value;
        pauseUI.blocksRaycasts = value;
    }
}
