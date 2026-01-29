using System;
using UnityEngine;
using static GameManager;

public class EventManager : MonoBehaviour
{
    // GameManager -> UIManager 상태 변경시 사용

    public static event Action OnGameOverUI;
    public static event Action<GameState> RequestGameState;
    public static event Action<string> RequestStageData;

    public void RequestGameStart(string stageNum) // UIManager에서 호출
    {
        RequestGameState?.Invoke(GameManager.GameState.Playing);
        RequestStageData?.Invoke(stageNum); 
    }

    public void RequestGamePause() // UIManager에서 호출
    {
        RequestGameState?.Invoke(GameManager.GameState.Pause);
    }

    public void RequestGameOver() // TimeManager + CoinHPManager에서 호출
    {
        OnGameOverUI?.Invoke();
        RequestGameState?.Invoke(GameManager.GameState.Idle);
    }

}
