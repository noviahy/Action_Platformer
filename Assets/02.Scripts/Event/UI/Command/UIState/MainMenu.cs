using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IUIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Button> menuBT;
    [SerializeField] private List<TextMeshProUGUI> worldText;
    [SerializeField] private List<Image> images;
    public void Enter()
    {
        setActiveAll(menuBT, true);
        setActiveAll(worldText, true);
        setActiveAll(images, true);
    }
    public void Exit()
    {
        setActiveAll(menuBT, false);
        setActiveAll(worldText, false);
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