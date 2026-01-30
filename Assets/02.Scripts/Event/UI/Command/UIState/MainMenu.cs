using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIState
{
    [SerializeField] private CanvasGroup mainMenuUI;
    public override EStateType StateType => EStateType.MainMenu;
    public override bool IsMenuState => true;
    public override void Enter()
    {
        setVisible(true);
    }
    public override void Exit()
    {
        setVisible(false);
    }

    private void setVisible(bool value)
    {
        mainMenuUI.alpha = value ? 1f : 0f;
        mainMenuUI.interactable = value;
        mainMenuUI.blocksRaycasts = value;
    }
}