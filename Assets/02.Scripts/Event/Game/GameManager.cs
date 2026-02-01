using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private StageProgressManager stageProgressManager;
    [SerializeField] private TimeManager timeManager;
    private string currentStageID;

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
    private void Awake()
    {
        ChangeState(GameState.Idle);
    }

    public void ChangeState(GameState next) // UIManager, GameManager
    {
        if (CurrentState == next) return;
        CurrentState = next;
        switch (next)
        {
            case GameState.Idle:
                EventManager.RequestGameState -= ChangeState;
                break;
            case GameState.Loading:
                EventManager.RequestGameState += ChangeState;
                EventManager.RequestStageID += RequestStageID;
                Debug.Log("GameState: " + CurrentState.ToString());
                StartCoroutine(waitForLoadingUI());
                break;

            case GameState.Playing:
                Time.timeScale = 1;

                timeManager.StartTimer();
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                timeManager.StopTimer();
                break;

            case GameState.Clear:
                eventManager.RequestClear();
                timeManager.StopTimer();
                RequestSaveData();
                break;

            default:
                Debug.LogError($"Unhandled UIState: {CurrentState}");
                break;
        }
    }

    public void RequestStageID(string stageID)
    {
        currentStageID = stageID;
    }
    public void SaveCollectionCoin()
    {
        stageProgressManager.SetCollectedCoin(currentStageID, true);
    }

    private void RequestSaveData()
    {
        stageProgressManager.SetCleared(currentStageID, true, timeManager.Timer);
    }
    IEnumerator waitForLoadingUI()
    {
        yield return new WaitForSeconds(3f);
        ChangeState(GameState.Playing);
    }
}
