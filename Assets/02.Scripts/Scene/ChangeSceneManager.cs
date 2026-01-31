using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneManager : MonoBehaviour
{ 
    private string currentStage;
    private void Awake()
    {
        EventManager.RequestStageID += changeScene;
        EventManager.RequestStageUI += changeScene;
    }
   
    private void changeScene(string stagID)
    {
        currentStage = StageSceneTable.GetSceneName(stagID);
        SceneManager.LoadScene(currentStage);
    }
}
