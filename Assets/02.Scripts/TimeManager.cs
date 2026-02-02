using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float DefaultTimeLimit;
    public float Timer { get; private set; }
    public float LeftTime { get; private set; }
    private Coroutine timerCoroutine;
    public void StartTimer()
    {
        if (timerCoroutine == null)
            timerCoroutine = StartCoroutine(TimerStart());
    }
    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void setDefaultTImer()
    {
        Timer = 0;
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
            if (Time.timeScale == 1)
            {
                Timer += Time.deltaTime;
                LeftTime = DefaultTimeLimit - Timer;
            }
            yield return null;
        }
    }
}
