using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage UIState.Playing UI in Playing and Loading
// UIState.Playing, Loading
public class PlayingUI : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text coin;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Image CollectionCoin;
    private Coroutine playingTextCoroutine;

    private void Start()
    {
        EventManager.RefreshPlayingData += refreshCoinHPText; // UIManager, CoinHPManager
    }

    private void refreshCoinHPText() // Change PlaingUI text (Event!)
    {
        life.text = $"{uiManager.RequestHP()}";
        coin.text = $"{uiManager.RequestCoin()}";
    }
    public void StartPlayingUI() // UIState(Playing(Pause ->Plaing), Loading(fade-out))
    {
        if (playingTextCoroutine == null)
        {
            playingTextCoroutine = StartCoroutine(PlayingUIText());
        }
    }
    public void StopPlayingUI() // (Playing.Exit())
    {
        if (playingTextCoroutine != null)
        {
            StopCoroutine(playingTextCoroutine);
            playingTextCoroutine = null;
        }
    }

    IEnumerator PlayingUIText()
    {
        while (true)
        {
            timer.text = Mathf.FloorToInt(timeManager.LeftTime).ToString(); // float -> int
            yield return null;
        }
    }
}
