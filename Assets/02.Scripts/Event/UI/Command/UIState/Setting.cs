using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : UIState
{
    [SerializeField] private CanvasGroup settingUI;
    public override EStateType StateType => EStateType.Setting;
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
        settingUI.alpha = value ? 1f : 0f;
        settingUI.interactable = value;
        settingUI.blocksRaycasts = value;
    }
}
