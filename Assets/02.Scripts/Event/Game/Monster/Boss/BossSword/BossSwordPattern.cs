using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossSwordPattern : MonoBehaviour
{
    [SerializeField] BossSword boss;
    [SerializeField] BossSwordAI bossAI;
    [SerializeField] private Collider2D rushHitBox;
    [SerializeField] private Collider2D defaultHitBox;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rushSpeed;
    private Coroutine coroutine;
    private Dictionary<BossSword.BossState, Func<IEnumerator>> actionMap;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rushHitBox.enabled =false;
        defaultHitBox.enabled =false;

        actionMap = new Dictionary<BossSword.BossState, Func<IEnumerator>>()
        {
        { BossSword.BossState.Idle, DoIdle },
        { BossSword.BossState.Walk, DoWalk },
        { BossSword.BossState.Jump, DoJump },
        { BossSword.BossState.Rush, DoRush},
        { BossSword.BossState.DefaultAttack, DoDefaultAttack },
        { BossSword.BossState.Slash, DoSlash}
        };
    }
    public void RequestAction(BossSword.BossState state)
    {
        if (coroutine != null)
            return;
        coroutine = StartCoroutine(actionMap[state]());
    }
    private void idle()
    {
        rb.linearVelocity = Vector2.zero;
    }
    private void jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void walk()
    {
        rb.linearVelocity = new Vector2(boss.moveX * walkSpeed, rb.linearVelocity.y);
    }
    private void rush()
    {
        while (rb.linearVelocity.x < boss.rushPoint.x)
        {
            rb.linearVelocity = new Vector2(boss.moveX * rushSpeed, rb.linearVelocity.y);
        }
    }
    IEnumerator DoIdle()
    {
        bossAI.GetIdelTime();
        yield return new WaitForSeconds(bossAI.GetIdelTime());
    }
    IEnumerator DoWalk()
    {
        walk();
        yield return bossAI.WalkTime;
    }
    IEnumerator DoJump()
    {
        jump();
        yield return bossAI.IdelTime;
    }
    IEnumerator DoRush() // 빠르게 돌진
    {
        rush();
        yield return null;
    }
    IEnumerator DoDefaultAttack()
    {
        bossAI.GetIdelTime();
        yield return bossAI.IdelTime;
    }
    IEnumerator DoSlash() // 짧게 베기
    {
        for (int i = 0; i < 3; i++)
        {
            defaultHitBox.enabled = true;
            walk();
            yield return new WaitForSeconds(0.4f);
            defaultHitBox.enabled = false;
            idle();
            yield return new WaitForSeconds(0.1f);
        }
    }
}