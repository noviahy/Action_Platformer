using UnityEngine;
using System.Collections;

public class BossSword : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BossSwordAI bossAI;
    private bool isActive = false;

    public enum BossState
    {
        Idle,
        walk,
        Jump,
        Rush,
        DefaultAttack,
        Slash,
    }
    public BossState CurrentState { get; private set; }
    public void ChangeBossState(BossState state)
    {
        StartCoroutine(DoIdle(state));
    }
    private void Update()
    {

    }
    IEnumerator DoIdle(BossState state) // Idle 상태완 다름
    {
        bossAI.GetIdelTime();
        yield return bossAI.IdelTime;
        CurrentState = state;
    }

}
