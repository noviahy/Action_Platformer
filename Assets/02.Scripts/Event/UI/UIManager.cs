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
    public UIState previousState{ get; private set; }
    public string SelectedWorld { get; private set; }
    public string SelectedStage { get; private set; }

    public void ChangeState(EStateType nextState)
    {
        if (!commandContainer.commandDict.TryGetValue(nextState, out var next))
        {
            Debug.LogError($"UIstate not found: {nextState}");
        }

        if (next.StateType == EStateType.StartUI)
        {
            currentState = next;
            next.Exit();
            return;
        }
        if(currentState.StateType == EStateType.StartUI)
        {
            currentState = next;
            currentState.Enter();
            return;
        }
        
        if (currentState == next) return;
        
        if (currentState.StateType == EStateType.Setting)
        {
            previousState = currentState;
        }
        else if (currentState.IsMenuState)
        {
            goBackStack.Push(currentState);
        }
        currentState?.Exit();

        previousState = currentState;
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
        switch (currentState)
        {
            case Loading:
                eventManager.RequestGameStart($"{SelectedWorld}-{SelectedStage}");
                break;

            case Pause:
                eventManager.RequestGamePause();
                break;

            case GameOver:
            case Clear:
                eventManager.RequestIdle();
                break;

            default:
                Debug.LogWarning("이상한 곳에서 호출 중");
                break;
        }
    }

    // 데이터 관련
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
