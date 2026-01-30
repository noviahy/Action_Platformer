using UnityEngine;
public class World : UIState
{
    [SerializeField] private CanvasGroup worldUI;
    public override EStateType StateType => EStateType.World;
    public override bool IsMenuState => true;
    public override void Enter()
    {
        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        worldUI.alpha = value ? 1f : 0f;
        worldUI.interactable = value;
        worldUI.blocksRaycasts = value;
    }
}