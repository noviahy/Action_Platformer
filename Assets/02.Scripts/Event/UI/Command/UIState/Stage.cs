using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stage : MonoBehaviour, IUIState
{
    // 추후 UI 설정시 코드 추가 필요
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Button> stageBT;
    [SerializeField] private List<TextMeshProUGUI> stageText;
    [SerializeField] private List<Image> images;
    public void Enter()
    {
        setActiveAll(stageBT, true);
        setActiveAll(stageText, true);
        setActiveAll(images, true);
    }
    public void Exit()
    {
        setActiveAll(stageBT, false);
        setActiveAll(stageText, false);
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
