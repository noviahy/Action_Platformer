using System.Collections;
using UnityEngine;

// GameManager
public class TimeManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float DefaultTimeLimit;
    public float Timer { get; private set; } // Used in GameManager to save Data
    public float LeftTime { get; private set; } // Used in UIManager to Set Playing UI
    private Coroutine timerCoroutine;
    public void StartTimer()  // GameManager(ChangeState: Playing)
    {
        if (timerCoroutine == null)
            timerCoroutine = StartCoroutine(TimerStart()); // Save Coroutine
    }
    public void StopTimer() // GameManager(ChangeState: Loading)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void setDefaultTImer() // GameManager(ChangeState: Loading)
    {
        Timer = 0;
        LeftTime = DefaultTimeLimit;
    }

    IEnumerator TimerStart()
    {
        while (true)
        {
            if (LeftTime <= 0f) // GameOver
            {
                LeftTime = 0; // Make sure it's zero
                eventManager.RequestGameOver("Time Over"); // Event! -> Change UI, GameState to GameOver
                yield break;
            }

            if (Time.timeScale == 1) // GameState == Playing
            {
                Timer += Time.deltaTime;
                LeftTime = DefaultTimeLimit - Timer;
            }
            yield return null;
        }
    }
}
