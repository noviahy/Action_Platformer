using UnityEngine;
using UnityEngine.UI;

// StageButtonBinder
public class StageButton : MonoBehaviour
{
    // The stageID set here does not affect the GameState or selection
    // This ID is used only for enabling/disabling and stage text
    [SerializeField] private string stageID;
    [SerializeField] private Button button;

    // private -> public
    public string StageID => stageID;

    public void SetInteractable(bool value) // StageButtonBinder
    {
        button.interactable = value;  // StageButton interactable
    }
}
