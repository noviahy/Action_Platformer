using System.Collections;
using UnityEngine;

public class Stage : UIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private CanvasGroup stageUI;
    [SerializeField] private CanvasGroup buttons;
    [SerializeField] private StageWindow stageWindow;
    public override EStateType StateType => EStateType.Stage;
    private float value = 1;
    public override void Enter()
    {
        if (uiManager.previousState.StateType != EStateType.World) // When previous State is Pause, GameOver, Clear
        {
            eventManager.RequestChangeScene(); // Change scene to UIScene
        }
        uiManager.RequestEvent(); // GameState.Idle
        uiManager.RefreshStageBT(); // StageButton isEnabled
        stageWindow.setDefaultWindowData(); // Set StageUIWindow

        setVisible(stageUI, true);
        setVisible(buttons, false);

        StartCoroutine(waitForStageButtons()); // Button fade - in
    }
    public override void Exit()
    {
        setVisible(stageUI, false);
        setVisible(buttons, false);
    }
    private void setVisible(CanvasGroup group, bool value)
    {
        group.alpha = value ? 1f : 0f;
        group.interactable = value;
        group.blocksRaycasts = value;
    }
    IEnumerator waitForStageButtons() // Button Fade - In
    {
        float elapsed = 0f;
        while (elapsed < value)
        {
            elapsed += Time.deltaTime;
            buttons.alpha = Mathf.Lerp(0f, 1f, elapsed / value);
            yield return null;
        }
        setVisible(buttons, true);
    }
}
