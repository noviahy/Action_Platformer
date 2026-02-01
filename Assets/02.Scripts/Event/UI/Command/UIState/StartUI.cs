using System.Collections;
using TMPro;
using UnityEngine;

public class StartUI : UIState
{
    [SerializeField] CanvasGroup startUI;
    [SerializeField] UIManager uiManager;
    [SerializeField] EventManager eventManager;
    [SerializeField] private TMP_Text Title;
    private float value = 1f;
    public override EStateType StateType => EStateType.StartUI;
    public override void Enter(){ }
    public override void Exit()
    {
        startUI.interactable = false;
        startUI.blocksRaycasts = false;
        StartCoroutine(waitForMainMenuUI());
    }
    IEnumerator waitForMainMenuUI()
    {
        startUI.alpha = 1f;
        float elapsed = 0f;
        while (elapsed < value)
        {
            elapsed += Time.deltaTime;
            startUI.alpha = Mathf.Lerp(1f, 0f, elapsed / value);
            yield return null;
        }

        startUI.alpha = 0f;

        eventManager.RequestChangeScene();
        uiManager.ChangeState(EStateType.MainMenu);
    }
}
