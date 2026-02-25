using UnityEngine;

public class BossSwordAI : MonoBehaviour
{
    public float IdelTime { get; private set; }

    public void GetIdelTime()
    {
        IdelTime = Random.Range(0.5f, 1f);
    }


}
