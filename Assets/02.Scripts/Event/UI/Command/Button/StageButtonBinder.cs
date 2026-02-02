using System.Collections.Generic;
using UnityEngine;

// Stage Button active
// UIManager
public class StageButtonBinder : MonoBehaviour
{
    [SerializeField] private List<StageButton> stageButtons;
    [SerializeField] private UIManager uiManager;
    private int number;

    public void Refresh() // UIManager
    {
        List<StageProgressData> worldData = uiManager.RequestStageDataList(); 
        number = 0;
        foreach (var stageButton in stageButtons)
        {
            stageButton.SetInteractable(worldData[number].isOpened);
            number++;
        }
    }
}