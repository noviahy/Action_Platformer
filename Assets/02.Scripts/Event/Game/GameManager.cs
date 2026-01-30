using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;

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
                StartCoroutine(waitForLoadingUI());
                break;

            case GameState.Playing:
                Time.timeScale = 1;
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                break;

            case GameState.Clear:
                eventManager.RequestClear();
                break;

            default:
                Debug.LogError($"Unhandled UIState: {CurrentState}");
                break;
        }
    }
    IEnumerator waitForLoadingUI()
    {
        yield return new WaitForSeconds(4f);
        ChangeState(GameState.Playing);
    }
}
