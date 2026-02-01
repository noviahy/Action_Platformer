using System.Collections;
using TMPro;
using UnityEngine;

public class Loading : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup loadingUI;
    [SerializeField] private CanvasGroup playingUI;
    [SerializeField] private TMP_Text stageID;
    [SerializeField] private TMP_Text HP;
    private float value = 1f;
    public override EStateType StateType => EStateType.Loading;
    public override bool IsMenuState => false;
    public override void Enter()
    {
        stageID.text = $"{uiManager.SelectedWorld}-{uiManager.SelectedStage}";
        HP.text = $"{uiManager.RequestHP()}";

        StartCoroutine(waitForLoadingUI());
        uiManager.RequestEvent();
        setVisible(true);
    }
    // ¾È ¾¸
    public override void Exit() { }
    private void setVisible(bool value)
    {
        playingUI.alpha = value ? 1f : 0f;
    }

    IEnumerator waitForLoadingUI()
    {    
        loadingUI.alpha = 1f;
        float elapsed = 0f;
        yield return new WaitForSeconds(2f);
        while (elapsed < value)
        {
            elapsed += Time.deltaTime;
            loadingUI.alpha = Mathf.Lerp(1f, 0f, elapsed / value);
            yield return null;
        }

        loadingUI.alpha = 0f;

        uiManager.ChangeState(EStateType.Playing);
    }
}
