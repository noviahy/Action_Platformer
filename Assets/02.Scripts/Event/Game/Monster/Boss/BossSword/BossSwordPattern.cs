using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossSwordPattern : MonoBehaviour
{
    [SerializeField] BossSword boss;
    [SerializeField] BossSwordTimer bossTimer;
    [SerializeField] BossCollision collisionHandler;

    [SerializeField] private GameObject rushHitBox;
    [SerializeField] private GameObject slashHitBox;
    [SerializeField] private GameObject defaultHitBox;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rushSpeed;

    private Coroutine coroutine;
    private Dictionary<BossSword.BossState, Func<IEnumerator>> actionMap;
    private Rigidbody2D rb;
    private float target;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rushHitBox.SetActive(false);
        slashHitBox.SetActive(false);
        defaultHitBox.SetActive(false);

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
        float angle = 30f * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(boss.moveX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);
    }
    private void walk()
    {
        rb.linearVelocity = new Vector2(boss.moveX * walkSpeed, rb.linearVelocity.y);
    }
    private void rush()
    {
        rb.linearVelocity = new Vector2(boss.moveX * rushSpeed, rb.linearVelocity.y);
    }
    IEnumerator DoIdle()
    {
        idle();
        yield return new WaitForSeconds(bossTimer.GetIdelTime());

        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoWalk()
    {
        idle();
        float timer = 0;
        float walkTime = bossTimer.GetWalkTime();
        while (timer <= walkTime)
        {
            walk();
            timer += Time.deltaTime;
            yield return null;
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoJump()
    {
        idle();
        jump();

        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoRush() // 빠르게 돌진
    {
        idle();
        target = boss.rushPoint.x;
        while (Mathf.Abs(rb.transform.position.x - target) > 0.3f)
        {
            rush();
            yield return null;
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoDefaultAttack()
    {
        idle();
        defaultHitBox.SetActive(true);
        yield return new WaitForSeconds(2f);
        defaultHitBox.SetActive(false);

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoSlash() // 짧게 베기
    {
        idle();
        for (int i = 0; i < 3; i++)
        {
            float time = 0;
            slashHitBox.SetActive(true);
            while (time <= 1f)
            {
                walk();
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;   
            slashHitBox.SetActive(false);
            while (time <= 0.5f)
            {
                idle();
                time += Time.deltaTime;
                yield return null;
            }
        }
        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
}