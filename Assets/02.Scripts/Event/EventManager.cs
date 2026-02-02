using System;
using UnityEngine;
using static GameManager;

public class EventManager : MonoBehaviour
{
    public static event Action OnGameOverUI;
    public static event Action OnGameClearUI;
    public static event Action RequestSaveData;
    public static event Action RefreshPlayingData;
    public static event Action<GameState> RequestGameState;
    public static event Action<string> GetReason;
    public static event Action<string> RequestStageID;
    public static event Action<string> RequestStageUI;


    public void RequestGameLoading(string stageID) // UIState(Playering에서 호출)
    {
        RequestGameState?.Invoke(GameManager.GameState.Loading); // (GameManager에서 구독)
        RequestStageID?.Invoke(stageID); // GameManager, ChangeSceneManager에서 구독
    }
    public void RequestGameRestart()
    {
        RequestGameState?.Invoke(GameManager.GameState.Playing);
    }

    public void RequestGamePause() // UIManager
    {
        RequestGameState?.Invoke(GameManager.GameState.Pause);
    }
    public void RequestGameOver(string reason) // Called from TimeManager + CoinHPManager
    {
        OnGameOverUI?.Invoke(); //  UIState(Clear, GameOver)
        RequestGameState?.Invoke(GameManager.GameState.GameOver);
        GetReason?.Invoke(reason); // GameOver.Enter()
    }
    public void RequestClear() // GameManager (GameState.Clear)
    {
        OnGameClearUI?.Invoke(); // UIState(Clear, GameOver)
    }
    public void RequestIdle() // Stage.Enter() , GameOver(Clear).Exit() -> Prevent data loss
    {
        RequestGameState?.Invoke(GameManager.GameState.Idle);

    }
    public void RequestSetCoinHPDatas() // GameManager (GameState.Clear && Idle)
    {
        RequestSaveData?.Invoke(); // CoinHPManager
    }

    public void RefreshPlayingUI() // CoinHPmanager에서 호출
    {
        RefreshPlayingData?.Invoke(); // PalyingUI에서 구독 (해제 안 함)
    }

    public void RequestChangeScene() // UIState.Stage,StartUI
    {
        RequestStageUI?.Invoke("UIScene"); // StageSceneManager
    }
}
