using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 게임 클리어 시 다음 스테이지로 자동 변환 해줘야 함
    // 기본 1-1
    // Text로 표현
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

    public void ChangeState(EStateType nextState)
    {
        if (!commandContainer.commandDict.TryGetValue(nextState, out var next))
        {
            Debug.LogError($"UIstate not found: {nextState}");
            return;
        }

        // 시작 시 한 번만 사용
        if (next.StateType == EStateType.StartUI)
        {
            currentState = next;
            next.Exit();
            return;
        }
        if (currentState.StateType == EStateType.StartUI)
        {
            currentState = next;
            currentState.Enter();
            return;
        }

        // 반복
        if (currentState == next)
        {
            Debug.LogError($"previousState == currentState: {nextState}");
            return;
        }

        if (currentState.IsMenuState)
        {
            goBackStack.Push(currentState);
        }
        var prev = currentState;
        currentState?.Exit();

        previousState = prev;
        currentState = next;
        currentState.Enter();
    }

    public void GoBackState()
    {
        if (currentState.StateType == EStateType.Setting)
        {
            ChangeState(previousState.StateType);
            return;
        }

        if (currentState.StateType == EStateType.Pause)
        {
            ChangeState(EStateType.Playing);
            return;
        }

        if (goBackStack.Count == 0) return;

        currentState.Exit();
        currentState = goBackStack.Pop();
        currentState.Enter();
    }
    public void RequestEvent()
    {
        switch (currentState.StateType)
        {
            case EStateType.Loading:
                eventManager.RefreshPlayingUI();
                eventManager.RequestGameLoading($"{SelectedWorld}-{SelectedStage}");
                break;

            case EStateType.Pause:
                eventManager.RequestGamePause();
                break;

            case EStateType.GameOver:
            case EStateType.Clear:
            case EStateType.Stage:
                eventManager.RequestIdle();
                break;

            default:
                Debug.LogWarning("이상한 곳에서 호출 중");
                break;
        }
    }

    // 데이터 관련 -> 나중에 코드를 나누던가 해야겠음
    public void RefreshStageBT()
    {
        stageButtonBinder.Refresh();
        // Debug.Log($"{SelectedStage}");
    }

    public void SetWorldNum(string worldNum)
    {
        SelectedWorld = worldNum;
        // Debug.Log($"{SelectedWorld}");
    }

    public void SetStageNum(string stageNum)
    {
        SelectedStage = stageNum;
    }
    public StageProgressData RequestStageData()
    {
        return stageData.GetStageData($"{SelectedWorld}-{SelectedStage}");
    }
    public List<StageProgressData> RequestStageDataList()
    {
        return stageData.GetWorldData(SelectedWorld);
    }
    public int RequestCoin()
    {
        return coinHPCalculator.Coin;
    }
    public int RequestHP()
    {
        return coinHPCalculator.HP;
    }
}
