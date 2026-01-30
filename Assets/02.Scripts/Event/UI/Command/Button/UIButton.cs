using UnityEngine;

// 모든 Button에 직접 넣기
public class UIButton : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EStateType stateType;
    [SerializeField] private string worldID;
    [SerializeField] private string stageID;
    public void OnClick()
    {
        uiManager.ChangeState(stateType);
    }
    // 현재 Stage 설정
    public void OnClickWorld()
    {
        uiManager.ChangeState(stateType);
        if (worldID != null)
        {
            uiManager.SetWorldNum(worldID);
        }
        else
        {
            uiManager.SetWorldNum(uiManager.SelectedWorld);
        }
    }
    // 현재 Stage 설정
    public void OnClickStage()
    {
        uiManager.SetStageNum(stageID);
    }
    // 뒤로가기
    public void OnClickBack()
    {
        uiManager.GoBackState();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}