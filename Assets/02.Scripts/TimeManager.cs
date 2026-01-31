using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float DefaultTimeLimit; 
    public float Timer { get; private set; }
    public float? LeftTime { get; private set; }
    private void Update() // ¾ðÁ¨°£ IEnumerable·Î ¹Ù²Ü°Å¿¡¿ä
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
}
