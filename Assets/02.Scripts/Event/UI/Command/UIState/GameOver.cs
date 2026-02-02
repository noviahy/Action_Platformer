using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup gameOverUI;
    [SerializeField] private TMP_Text reasonText;
    public override EStateType StateType => EStateType.GameOver;
    public override void Enter()
    {
        EventManager.GetReason += gameOverReason;
        setVisible(true);
    }
    public override void Exit()
    {
        uiManager.RequestEvent();
        EventManager.OnGameOverUI -= () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI -= () => uiManager.ChangeState(EStateType.Clear);
        EventManager.GetReason -= gameOverReason;
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        gameOverUI.alpha = value ? 1f : 0f;
        gameOverUI.interactable = value;
        gameOverUI.blocksRaycasts = value;
    }

    private void gameOverReason(string reason)
    {
        reasonText.text = reason;
    }
}