using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    // 여기서 설정해 주는 stageID는 GameState와 선택에 영향 없음
    // 활성화 비활성화 + Stage Text에만 사용하는 ID
    [SerializeField] private string stageID;
    [SerializeField] private Button button;

    // private를 public같이 사용
    public string StageID => stageID;

    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}
