using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private StageProgressManager stageProgressManager;
    [SerializeField] private CoinHPProgressManager coinHPProgressManager;
    [SerializeField] private TimeManager timeManager;
    private string currentStageID;
    private bool onTimer = false; // 이런건 나중에 timeManager에서 직접 바꾸게 만드는게 좋을 듯

    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public enum GameState
    {
        Idle,
        Loading,
        Playing,
        Pause,
        GameOver,
        Clear
    }
    private void Start()
    {
        ChangeState(GameState.Idle);
        EventManager.RequestGameState += ChangeState; // ChangState in UIManager
        EventManager.RequestStageID += RequestStageID; // Get ID from UIManager
        Instance = this;
    }

    public void ChangeState(GameState next) // UIManager, GameManager
    {
        if (CurrentState == next) return;
        CurrentState = next;

        switch (next)
        {
            case GameState.Idle:
                RequestSaveCoinHPData();  // Save CoinHP Data on computer (duplication execution: Clear)
                timeManager.StopTimer(); // Stop Timer Coroutine
                Time.timeScale = 1; // usefull Pause -> Idle
                break;

            case GameState.Loading:
                onTimer = false; // Prevent Coroutine Overlapping
                timeManager.setDefaultTImer(); // Initialization timer
                break;

            case GameState.Playing:
                Time.timeScale = 1; // usefull Pause -> Playing
                if (!onTimer) // only executed Pause -> Playing
                {
                    timeManager.StartTimer(); // Start TimerManager Coroutine
                    onTimer = true;
                }
                break;

            case GameState.Pause:
                Time.timeScale = 0; // stop
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                timeManager.StopTimer(); // Stop TimeManager Coroutine
                break;

            case GameState.Clear:
                eventManager.RequestClear(); // Change UIState.Clear (!Event)
                timeManager.StopTimer(); // Stop Timer Coroutine
                RequestSetClearData(); // Set StageData on StageProgressManager
                RequestSaveStageData(); // Save Stage Data on computer 
                break;

            default:
                Debug.LogError($"Unhandled UIState: {CurrentState}"); // Error~
                break;
        }
    }
    public void RequestStageID(string stageID) // Get StageID form UIManager(UIState.Loading) (Event!)
    {
        currentStageID = stageID; // Save it in GameManager.currentStageID
    }
    public void SaveCollectionCoin() // CollectionCoin.cs -> OnTriggerEnter with Player
    {
        stageProgressManager.SetCollectedCoin(currentStageID, true);
        stageProgressManager.SaveAll(); // Save all Data on computer
    }

    // private
    private void RequestSetClearData() // When GameState.Clear
    {
        stageProgressManager.SetCleared(currentStageID, true, timeManager.Timer); // Set StageData on StageProgressManager
    }
    private void RequestSaveStageData() // When GameState.Clear
    {
        stageProgressManager.SaveAll(); // Save all Data on computer
        RequestSaveCoinHPData();  // Save CoinHP Data on computer
    }
    private void RequestSaveCoinHPData() // When GameState.Clear && Idle
    {
        eventManager.RequestSetCoinHPDatas(); // set CoinHp Data on CoinHPProgressmanager (Event!)
        coinHPProgressManager.Save(); // save 
    }
}
