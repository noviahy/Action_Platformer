using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private StageProgressManager stageData;
    [SerializeField] private CommandContainer commandContainer;
    [SerializeField] private StageButtonBinder stageButtonBinder;
    [SerializeField] private CoinHPCalculator coinHPCalculator;
    private Stack<UIState> goBackStack = new Stack<UIState>();
    private UIState currentState;
    public UIState previousState { get; private set; }
    public string SelectedWorld { get; private set; }
    public string SelectedStage { get; private set; }

    public void ChangeState(EStateType nextState) // UIButton, Loading, StartUI ...
    {
        if (!commandContainer.commandDict.TryGetValue(nextState, out var next)) // Find nextState in commandContainer
        {
            Debug.LogError($"UIstate not found: {nextState}");
            return;
        }

        // At initialization, there is no currentState, so a button click sets the state to StartUI.
        if (next.StateType == EStateType.StartUI)
        {
            currentState = next;
            // Then StartUI.Exit() is executed, since StartUI does not need an Enter phase.
            next.Exit();
            return;
        }
        // When changing the state from StartUI to MainMenu, only Enter() is called,
        // because StartUI.Exit() has already been executed.
        if (currentState.StateType == EStateType.StartUI)
        {
            currentState = next;
            currentState.Enter();
            return;
        }

        // Repeat
        if (currentState == next) // Prevent same State
        {
            Debug.LogError($"previousState == currentState: {nextState}");
            return;
        }

        if (currentState.IsMenuState) // Stack (Only MainMenu and World)
        {
            goBackStack.Push(currentState);
        }
        var prev = currentState;
        currentState?.Exit();

        previousState = prev;
        currentState = next;
        currentState.Enter();
    }

    public void GoBackState() // Back Button
    {
        if (currentState.StateType == EStateType.Setting) // CurrentState -> Setting
        {
            ChangeState(previousState.StateType); // using previousState
            return;
        }

        if (currentState.StateType == EStateType.Pause) // CurrentState -> Pause
        {
            ChangeState(EStateType.Playing); // Just Playing
            return;
        }

        if (currentState.StateType == EStateType.Playing)
        {
            ChangeState(EStateType.Pause);
            return;
        }

        if (goBackStack.Count == 0) return; // Stack is empty

        // else: Stage, World
        currentState.Exit();
        currentState = goBackStack.Pop();
        currentState.Enter();
    }
    public void RequestEvent() // Reunning event here (UIState class)
    {
        switch (currentState.StateType)
        {
            case EStateType.Loading:
                // Since the loading state fades out, the PlayingUI must be prepared beforehand
                eventManager.RefreshPlayingUI(); // set PlayingUI Coin HP Text
                eventManager.RequestGameLoading($"{SelectedWorld}-{SelectedStage}"); // deliver StageID to GameManager
                break;

            case EStateType.Pause:
                eventManager.RequestGamePause(); // GameState.Pause -> Time.timescale = 0
                break;

            case EStateType.GameOver:
            case EStateType.Clear:
            case EStateType.Stage:
                eventManager.RequestIdle();// GameState.Idle (Save CoinHP Data on computer, Stop Timer Coroutine)
                break;

            case EStateType.Playing:
                eventManager.RequestGameRestart(); // GameState.Playing (Start TimerManager Coroutine)
                break;

            default:
                Debug.LogWarning("이상한 곳에서 호출 중"); // Error~
                break;
        }
    }

    // Data-related logic -> may need to be separated later
    public void RefreshStageBT()
    {
        stageButtonBinder.Refresh();
    }

    public void SetWorldNum(string worldNum) // UIButton (World button)
    {
        SelectedWorld = worldNum;
    }

    public void SetStageNum(string stageNum) // UIButton(used whan num change), StageWindow(Set default num)
    {
        SelectedStage = stageNum; // save UIManager.SelectedStage
    }
    public StageProgressData RequestStageData() // StageWindow
    {
        return stageData.GetStageData($"{SelectedWorld}-{SelectedStage}");
    }
    public List<StageProgressData> RequestStageDataList() // StageButtonBinder(Stage Button Active), StageWindow
    {
        return stageData.GetWorldData(SelectedWorld);
    }
    public int RequestCoin() // UIState(Playing, Stage)
    {
        return coinHPCalculator.Coin;
    }
    public int RequestHP() // UIState(Playing, Stage, Loading)
    {
        return coinHPCalculator.HP;
    }
}
