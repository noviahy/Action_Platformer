using UnityEngine;
using UnityEngine.UI;

public class StartUI : UIState
{
    [SerializeField] CanvasGroup startUI;
    [SerializeField] Button startButton;

    public override EStateType StateType => EStateType.StartUI;
    public override void Enter() { }
    public override void Exit() { }
    private void OnClickStartBT()
    {

    }

}
