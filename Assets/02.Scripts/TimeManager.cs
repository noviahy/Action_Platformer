using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float DefaultTimeLimit;
    public float Timer { get; private set; }
    public float LeftTime { get; private set; }

    private void Start()
    {
        LeftTime = DefaultTimeLimit;
    }
    public void StartTimer()
    {
        LeftTime = DefaultTimeLimit;
        StartCoroutine(TimerStart());
    }
    public void StopTimer()
    {
        StopCoroutine(TimerStart());
        LeftTime = DefaultTimeLimit;
    }

    IEnumerator TimerStart()
    {
        while (true)
        {
            if (LeftTime <= 0f)
            {
                LeftTime = 0;
                eventManager.RequestGameOver("Time Over");
                LeftTime = DefaultTimeLimit;
                yield break;
            }

            Timer += Time.deltaTime;
            LeftTime = DefaultTimeLimit - Timer;

            yield return null;
        }
    }
}
