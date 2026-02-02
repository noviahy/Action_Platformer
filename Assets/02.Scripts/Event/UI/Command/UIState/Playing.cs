using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Playing : UIState
{
    [SerializeField] private CanvasGroup playingUI;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayingUI playingUIText;
    public override EStateType StateType => EStateType.Playing;
    public override void Enter()
    {
        // Subscribe events
        EventManager.OnGameOverUI += () => uiManager.ChangeState(EStateType.GameOver);
        EventManager.OnGameClearUI += () => uiManager.ChangeState(EStateType.Clear);

        uiManager.RequestEvent(); // GameState.Playing
        playingUIText.StartPlayingUI(); // Start PlayingUI Text Coroutine (used when Pause -> Playing)

        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
        playingUIText.StopPlayingUI(); // Stop Coroutine
    }

    private void setVisible(bool value)
    {
        playingUI.alpha = value ? 1f : 0f;
        playingUI.interactable = value;
        playingUI.blocksRaycasts = value;
    }
}
