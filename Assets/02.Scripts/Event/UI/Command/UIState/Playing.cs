using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Playing : UIState
{
    [SerializeField] private CanvasGroup playingUI;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text coin;
    public override EStateType StateType => EStateType.Playing;
    public override void Enter()
    {
        EventManager.OnGameOverUI += () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI += () => uiManager.ChangeState(EStateType.Clear);
        setVisible(true);

        StartCoroutine(timerText());
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
