using UnityEngine;

// Button에 직접 넣기 + 이벤트 넣기
public class UIButton : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EStateType stateType;
    [SerializeField] private string worldNum;
    [SerializeField] private string stageNum;
    public void OnClick()
    {
        uiManager.ChangeState(stateType);
    }
    // 현재 Stage 설정
    public void OnClickWorld()
    {
        uiManager.ChangeState(stateType);
        uiManager.SetWorldNum(worldNum);
    }
    // 현재 Stage 설정
    public void OnClickStage()
    {
        uiManager.SetStageNum(stageNum);
    }
}

/*  [SerializeField] private Button world;
    [SerializeField] private Button world_1;
    [SerializeField] private Button world_2;
    [SerializeField] private Button world_3;
    [SerializeField] private Button stage_1;
    [SerializeField] private Button stage_2;
    [SerializeField] private Button stage_3;
    [SerializeField] private Button settingBT;
    [SerializeField] private Button pauseBT;
    [SerializeField] private Button continueBT;
    [SerializeField] private Button gameStartBT;
    [SerializeField] private Button gameStopBT;
    [SerializeField] private Button backToStageBT;
    [SerializeField] private Button backBT; 
*/