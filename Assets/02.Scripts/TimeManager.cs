using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float DefaultTimeLimit;
    public float Timer { get; private set; }
    public float? LeftTime { get; private set; }

    public void StartTimer()
    {
        StartCoroutine(startTimer());
    }
    public void StopTimer()
    {
        StopCoroutine(startTimer());
    }

    IEnumerator startTimer()
    {
        if (LeftTime <= 0f)
        {
            LeftTime = 0;
            eventManager.RequestGameOver();
            yield break;
        }
        
        Timer += Time.deltaTime;
        LeftTime = DefaultTimeLimit - Timer;
        
        yield return null;
    }
}
