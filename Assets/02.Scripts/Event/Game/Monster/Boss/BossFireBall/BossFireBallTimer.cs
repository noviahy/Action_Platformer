using UnityEngine;

public class BossFireBallTimer : MonoBehaviour
{
    public int PatternNum { get; private set; }
    public float IdelTime { get; private set; }
    public float WalkTime { get; private set; }

    public float GetIdelTime()
    {
        return IdelTime = Random.Range(0.5f, 1f);
    }
    public float GetWalkTime()
    {
        return WalkTime = Random.Range(1f, 3f);
    }
}
