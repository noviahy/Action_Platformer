using UnityEngine;

public class BossCannonTimer : MonoBehaviour
{
    public float IdelTime { get; private set; }
    public float WalkTime { get; private set; }
    public int JumpTime { get; private set; }
    public float GetIdelTime()
    {
        return IdelTime = Random.Range(0.5f, 2f);
    }
    public float GetWalkTime()
    {
        return WalkTime = Random.Range(1f, 4f);
    }
    public float GetJumpTime()
    {
        return JumpTime = Random.Range(1, 4);
    }

}
