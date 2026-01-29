using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private float DefaultTimeLimit; // Stage의 TimeLimit가 Null이면 DefaultTimerLimit 사용
    public float Timer { get; private set; }
    public float? LeftTime { get; private set; }

    private void Awake()
    {
        EventManager.RequestStageData += initTimer;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing) return;

        Timer += Time.deltaTime;
        LeftTime = DefaultTimeLimit - Timer;

        if (LeftTime <= 0f)
        {
            LeftTime = 0;
            eventManager.RequestGameOver();
        }
    }
    private void initTimer(string stageID) // StageData 시간
    {
        StageData stageData = stageManager.GetStageData(stageID);
        Timer = 0;
        if (stageData.timeLimit != null) // 추후 데이터 추가 예정
        {
            LeftTime = stageData.timeLimit;
        }
        else
        {
            LeftTime = DefaultTimeLimit;
        }
    }

}
