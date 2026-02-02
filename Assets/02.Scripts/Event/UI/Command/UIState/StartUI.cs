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
    public override void Enter()
    {
        // Normally, data should be loaded here, but this is left empty for now
        // nothing comes to mind yet
    }
    public override void Exit()
    {
        startUI.interactable = false;
        startUI.blocksRaycasts = false;
        StartCoroutine(waitForMainMenuUI());
    }
    IEnumerator waitForMainMenuUI() // Fade - Out
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

        eventManager.RequestChangeScene(); // Change scene to UIScene
        // When changing the state from StartUI to MainMenu, only Enter() is called,
        // because StartUI.Exit() has already been executed.
        uiManager.ChangeState(EStateType.MainMenu);
    }
}
