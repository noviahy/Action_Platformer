using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : MonoBehaviour, IUIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Button> gameOverBT;
    [SerializeField] private List<TextMeshProUGUI> gameOverText;
    [SerializeField] private List<Image> images;
    public void Enter()
    {
        setActiveAll(gameOverBT, true);
        setActiveAll(gameOverText, true);
        setActiveAll(images, true);
    }
    public void Exit()
    {
        setActiveAll(gameOverBT, false);
        setActiveAll(gameOverText, false);
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