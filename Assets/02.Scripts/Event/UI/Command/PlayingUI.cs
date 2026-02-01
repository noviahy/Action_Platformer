using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingUI : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text coin;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Image CollectionCoin;

    private void Start()
    {
        EventManager.RefreshPlayingData += refreshCoinHPText;
    }

    private void refreshCoinHPText()
    {
        life.text = uiManager.RequestHP().ToString();
        coin.text = uiManager.RequestCoin().ToString();
    }
    public void StartPlayingUI()
    {
        
        StartCoroutine(PlayingUIText());
    }
    public void StopPlayingUI()
    {
        StopCoroutine(PlayingUIText());
    }

    IEnumerator PlayingUIText()
    {
        while (true)
        {
            timer.text = timeManager.LeftTime.ToString();
            Debug.Log(timer.text);

            yield return null;
        }
    }
}
