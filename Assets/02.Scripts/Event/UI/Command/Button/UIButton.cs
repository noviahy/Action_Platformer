using UnityEngine;

// All Button Code
public class UIButton : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EStateType stateType;
    [SerializeField] private string worldID;
    [SerializeField] private string stageID;
    [SerializeField] private StageWindow stageWindow;
    public void OnClick() // Default Button Event
    {
        uiManager.ChangeState(stateType);
    }
    public void OnClickWorld() // World Button
    {
        // used this code to go Pause -> World, But changeed it to OnClick()
        if (worldID != "") // Use String (Doesn't use)
        {
            uiManager.SetWorldNum(worldID); // Change worldID in UIManager
        }
        uiManager.ChangeState(stateType); 
    }
    public void OnClickStage() // StageButton (Doesn't Change State)
    {
        uiManager.SetStageNum(stageID); // Save StageID in UIManager
        stageWindow.SetWindowData(); // Change StageWindow
    }
    public void OnClickBack() // Back Button
    {
        uiManager.GoBackState(); 
    }

    public void OnClickExit() // Quit Game Button
    {
        Application.Quit();
    }
}