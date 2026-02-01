using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Playing : UIState
{
    [SerializeField] private CanvasGroup playingUI;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text coin;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Image CollectionCoin;
    public override EStateType StateType => EStateType.Playing;
    public override void Enter()
    {
        EventManager.OnGameOverUI += () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI += () => uiManager.ChangeState(EStateType.Clear);

        StartCoroutine(PlayingUIText());
        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
        StopCoroutine(PlayingUIText());
    }

    private void setVisible(bool value)
    {
        playingUI.alpha = value ? 1f : 0f;
        playingUI.interactable = value;
        playingUI.blocksRaycasts = value;
    }
    IEnumerator PlayingUIText()
    {
        timer.text = $"{timeManager.LeftTime}";
        life.text = $"{uiManager.RequestHP()}";
        coin.text = $"{uiManager.RequestCoin()}";

        yield return null;
    }
}
