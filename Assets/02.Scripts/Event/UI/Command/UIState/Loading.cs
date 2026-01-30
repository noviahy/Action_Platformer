using System.Collections;
using UnityEngine;

public class Loading : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup loadingUI;
    private float value = 1f;
    public override EStateType StateType => EStateType.Loading;
    public override bool IsMenuState => false;
    public override void Enter()
    {
        // 씬 변경 코드 추가해야함

        uiManager.RequestEvent();
        StartCoroutine(waitForLoadingUI());
    }
    // 안 씀
    public override void Exit() { }

    IEnumerator waitForLoadingUI()
    {
        float elapsed = 0f;
        while (elapsed < value)
        {
            elapsed += Time.deltaTime;
            loadingUI.alpha = Mathf.Lerp(0f, 1f, elapsed / value);
            yield return null;
        }

        loadingUI.alpha = 1f;
        
        elapsed = 0f;
        yield return new WaitForSeconds(2f);
        while (elapsed < value)
        {
            elapsed += Time.deltaTime;
            loadingUI.alpha = Mathf.Lerp(1f, 0f, elapsed / value);
            yield return null;
        }

        loadingUI.alpha = 1f;

        uiManager.ChangeState(EStateType.Playing);
    }
}
