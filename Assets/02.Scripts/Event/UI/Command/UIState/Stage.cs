using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
        if (uiManager.previousState.StateType != EStateType.World)
        {
            eventManager.RequestChangeScene();
        }
        uiManager.RequestEvent();
        uiManager.RefreshStageBT();
        stageWindow.setDefaultWindowData();

        setVisible(stageUI, true);
        setVisible(buttons, false);

        StartCoroutine(waitForStageButtons());
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
    IEnumerator waitForStageButtons()
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
