using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Loading : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayingUI playingUIText;
    [SerializeField] private CanvasGroup loadingUI;
    [SerializeField] private CanvasGroup playingUI;
    [SerializeField] private TMP_Text stageID;
    [SerializeField] private TMP_Text HP;

    private float value = 1f;
    public override EStateType StateType => EStateType.Loading;
    public override bool IsMenuState => false; // defalut Could be removed, but left as is
    public override void Enter()
    {
        // Loading Text
        stageID.text = $"{uiManager.SelectedWorld}-{uiManager.SelectedStage}"; 
        HP.text = $"X {uiManager.RequestHP()}";

        uiManager.RequestEvent(); // GameState.Loading, give StageID to GameManager and ChangeSceneManager
        playingUIText.StartPlayingUI(); // Start coroutine (Preload the PlayingUI)

        StartCoroutine(waitForLoadingUI()); // Start Fade - OUt
        setVisible(true);
    }
    public override void Exit() { }
    private void setVisible(bool value) // Just alpha 
    {
        playingUI.alpha = value ? 1f : 0f;
    }

    IEnumerator waitForLoadingUI() // Fade - Out
    {
        loadingUI.alpha = 1f;
        float elapsed = 0f;
        yield return new WaitForSeconds(2f); // Wait 2 seconds

        while (elapsed < value) // start fade - out
        {
            elapsed += Time.deltaTime;
            loadingUI.alpha = Mathf.Lerp(1f, 0f, elapsed / value);
            yield return null;
        }

        loadingUI.alpha = 0f;
        uiManager.ChangeState(EStateType.Playing); // UIState.Playing
    }
}
