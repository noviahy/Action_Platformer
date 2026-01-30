using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup gameOverUI;
    public override EStateType StateType => EStateType.GameOver;
    public override void Enter()
    {
        setVisible(true);
    }
    public override void Exit()
    {
        EventManager.OnGameOverUI -= () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI -= () => uiManager.ChangeState(EStateType.Clear);
        uiManager.RequestEvent();
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        gameOverUI.alpha = value ? 1f : 0f;
        gameOverUI.interactable = value;
        gameOverUI.blocksRaycasts = value;
    }
}