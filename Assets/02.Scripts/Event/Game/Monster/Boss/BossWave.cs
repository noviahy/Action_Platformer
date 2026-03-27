using UnityEngine;

public class BossWave : MonoBehaviour
{
    [SerializeField] private int aliveCount;
    private Player player;
    [SerializeField] private Bar[] bars;
    [SerializeField] private Transform activePoint;
    private bool isActive = false;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x - activePoint.position.x) < 0.3f && !isActive)
        {
            isActive = true;
            OnBarsActive();
        }
    }
    private void OnBarsActive()
    {
        foreach (var bar in bars)
            bar.RequestActive(true);
    }
    public void OnBossDead()
    {
        aliveCount--;

        if (aliveCount <= 0)
        {
            foreach (Bar bar in bars)
                bar.RequestActive(false);
        }
    }

}
