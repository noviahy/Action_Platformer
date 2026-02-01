using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MainMenu : UIState
{
    [SerializeField] private CanvasGroup mainMenuUI;
    [SerializeField] private CanvasGroup buttons;
    private float value = 0.5f;
    public override EStateType StateType => EStateType.MainMenu;
    public override bool IsMenuState => true;
    public override void Enter()
    {
        setVisible(mainMenuUI, true);
        setVisible(buttons, false);
        StartCoroutine(waitForButtons());
    }
    public override void Exit()
    {
        setVisible(mainMenuUI, false);
        setVisible(buttons, false);
    }

    private void setVisible(CanvasGroup group, bool value)
    {
        group.alpha = value ? 1f : 0f;
        group.interactable = value;
        group.blocksRaycasts = value;
    }
    IEnumerator waitForButtons()
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