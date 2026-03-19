using UnityEngine;

public class BossWave : MonoBehaviour
{
    [SerializeField] private int aliveCount;
    [SerializeField] private Bar[] bars;
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
