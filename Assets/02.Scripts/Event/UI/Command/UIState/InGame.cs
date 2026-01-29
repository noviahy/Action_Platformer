using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour, IUIState
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Button> inGameBT;
    [SerializeField] private List<TextMeshProUGUI> inGameText;
    [SerializeField] private List<Image> images;
    public void Enter()
    {
        // 게임 데이터 불러오기
        StartCoroutine(timerText());

        setActiveAll(inGameBT, true);
        setActiveAll(inGameText, true);
        setActiveAll(images, true);

    }

    IEnumerator timerText()
    {
        yield return null;
    }
    public void Exit()
    {
        setActiveAll(inGameBT, false);
        setActiveAll(inGameText, false);
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
