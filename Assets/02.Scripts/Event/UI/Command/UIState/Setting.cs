using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour, IUIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Button> settingBT;
    [SerializeField] private List<TextMeshProUGUI> settingText;
    [SerializeField] private List<Image> images;
    public void Enter()
    {
        setActiveAll(settingBT, true);
        setActiveAll(settingText, true);
        setActiveAll(images, true);
    }
    public void Exit()
    {
        setActiveAll(settingBT, false);
        setActiveAll(settingText, false);
        setActiveAll(images, false);
    }

    private void setActiveAll<T>(List<T> targets, bool active) where T : Component
    {
        foreach (var t in targets)
        {
            t.gameObject.SetActive(active);
        }
    }
}
