using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private DespawnManager despawnManager;
    public GameState CurrentState { get; private set; }
    public enum GameState
    {
        Playing,
        Pause,
        GameOver,
        Idle
        // 추후 Loading 추가
    }
    private void Awake()
    {
        ChangeState(GameState.Idle);
        EventManager.RequestGameState += ChangeState;
        EventManager.RequestStageData += RequestSpawnStage;
    }

    public void ChangeState(GameState next) // UIManager, GameManager
    {
        if (CurrentState == next) return;
        CurrentState = next;
        switch (next)
        {
            case GameState.Idle:
                requestDespawnStage();
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                break;

            /*
            case GameState.Loading:
            break;
            */

            default:
                Debug.LogError($"Unhandled UIState: {CurrentState}");
                break;
        }
    }

    public void RequestSpawnStage(string stageNum)
    {
        spawnManager.SpawnStage(stageNum);
    }
    private void requestDespawnStage()
    {
        despawnManager.DespawnAllObject();
    }
}
