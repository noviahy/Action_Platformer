using System.Collections.Generic;
using UnityEngine;

public class StageButtonBinder : MonoBehaviour
{
    [SerializeField] private StageProgressManager progressManager;
    [SerializeField] private List<StageButton> stageButtons;
    [SerializeField] private UIManager uiManager;

    public void Refresh()
    {
        foreach (var stageButton in stageButtons)
        {
            var data = progressManager.GetStageData(uiManager.SelectedWorld + "-" + stageButton.StageID);

            if (data == null)
            {
                stageButton.SetInteractable(false);
                continue;
            }
            stageButton.SetInteractable(data.isOpened);
        }
    }
}