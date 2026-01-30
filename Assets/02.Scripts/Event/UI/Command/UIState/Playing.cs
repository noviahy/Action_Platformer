using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Playing : UIState
{
    [SerializeField] private CanvasGroup playingUI;
    [SerializeField] private UIManager uiManager;
    public override EStateType StateType => EStateType.Playing;
    public override void Enter()
    {
        EventManager.OnGameOverUI += () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI += () => uiManager.ChangeState(EStateType.Clear);
        uiManager.RequestEvent();

        StartCoroutine(timerText());
        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        playingUI.alpha = value ? 1f : 0f;
        playingUI.interactable = value;
        playingUI.blocksRaycasts = value;
    }
    IEnumerator timerText()
    {
        yield return null;
    }
}
