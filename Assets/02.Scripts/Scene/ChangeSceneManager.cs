using UnityEngine;
using UnityEngine.SceneManagement;

// EventManager
public class ChangeSceneManager : MonoBehaviour
{
    private string currentStage;
    private void Awake()
    {
        // UIManager (UIState:Loading) Get StageID from UIManager
        EventManager.RequestStageID += changeScene;
        // InGame -> Stage, StartUI -> MainMenu 
        EventManager.RequestStageUI += changeScene; // Change scene to UIScene
    }
    private void changeScene(string stagID)
    {
        currentStage = StageSceneTable.GetSceneName(stagID); // Get Scene
        SceneManager.LoadScene(currentStage); // Change Scene
    }
}
