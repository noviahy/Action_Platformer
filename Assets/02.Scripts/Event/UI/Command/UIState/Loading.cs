using System.Collections;
using UnityEngine;

public class Loading : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup loadingUI;
    [SerializeField] private CanvasGroup playingUI;
    private float value = 1f;
    public override EStateType StateType => EStateType.Loading;
    public override bool IsMenuState => false;
    public override void Enter()
    {
        // 씬 변경 코드 추가해야함
        StartCoroutine(waitForLoadingUI());
        uiManager.RequestEvent();
        setVisible(true);
    }
    // 안 씀
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
