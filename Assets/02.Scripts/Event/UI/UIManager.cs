using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 게임 클리어 시 다음 스테이지로 자동 변환 해줘야 함
    // 기본 1-1
    // Text로 표현
    public string SelectedWorld { get; private set; }
    public string SelectedStage { get; private set; }
    [SerializeField] private CommandContainer commandContainer;
    private Stack<IUIState> goBackStack = new Stack<IUIState>();
    private IUIState currentState;


    private void Awake()
    {
        if (currentState == null)
        {
            EventManager.OnGameOverUI += () => ChangeState(EStateType.mainMenu);
        }
    }
    public void ChangeState(EStateType nextState)
    {
        if (currentState != null)
        {
            goBackStack.Push(currentState);
            currentState?.Exit();
        }

        if (commandContainer.commandDict.TryGetValue(nextState, out var command))
        {
            currentState = command; 
            currentState.Enter();
        }
    }
    public void GoBackState()
    {
        if (goBackStack.Count == 0) return;

        currentState.Exit();
        currentState = goBackStack.Pop();
        currentState.Enter();
    }

    public void SetWorldNum(string worldNum)
    {
        SelectedWorld = worldNum;
    }

    public void SetStageNum(string stageNum) 
    {
        SelectedStage = stageNum;
    }

}
