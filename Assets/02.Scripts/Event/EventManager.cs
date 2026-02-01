using System;
using UnityEngine;
using static GameManager;

public class EventManager : MonoBehaviour
{
    public static event Action OnGameOverUI;
    public static event Action OnGameClearUI;
    public static event Action RequestSaveData;
    public static event Action<GameState> RequestGameState;
    public static event Action<string> RequestStageID;
    public static event Action<string> RequestStageUI;
    

    public void RequestGameStart(string stageID) // UIState(Playering에서 호출)
    {
        RequestGameState?.Invoke(GameManager.GameState.Loading); // (GameManager에서 구독)

        RequestStageID?.Invoke(stageID); // GameManager, ChangeSceneManager에서 구독
    }

    public void RequestGamePause() // UIManager에서 호출
    {
        RequestGameState?.Invoke(GameManager.GameState.Pause);
    }
    public void RequestGameOver() // TimeManager + CoinHPManager에서 호출
    {
        OnGameOverUI?.Invoke(); //  UIState(Clear, GameOver에서 구독/취소)
        RequestGameState?.Invoke(GameManager.GameState.GameOver);
    }
    public void RequestClear() // GameManger에서 호출
    {
        OnGameClearUI?.Invoke(); // UIState(Clear, GameOver에서 구독/취소)
    }
    public void RequestIdle()
    {
        RequestGameState?.Invoke(GameManager.GameState.Idle);
        RequestSaveData?.Invoke(); // (취소 안 함)CoinHPManager에서 구독
    }

    public void RequestChangeScene()
    {
        RequestStageUI?.Invoke("UIScene");
    }
}
