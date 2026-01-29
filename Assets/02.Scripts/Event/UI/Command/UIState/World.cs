using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class World : MonoBehaviour, IUIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Button> worldBT;
    [SerializeField] private List<TextMeshProUGUI> worldText;
    [SerializeField] private List<Image> images;
    public void Enter()
    {
        setActiveAll(worldBT, true);
        setActiveAll(worldText, true);
        setActiveAll(images, true);
    }
    public void Exit()
    {
        setActiveAll(worldBT, false);
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