using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BossCannonPattern : MonoBehaviour
{
    [SerializeField] BossCannon boss;
    [SerializeField] BossCannonTimer bossTimer;
    [SerializeField] BossCollision collisionHandler;
    [SerializeField] private Explosion explosion;

    [SerializeField] private Collider2D defaultScoket;
    [SerializeField] private Collider2D slashHitBox;
    [SerializeField] private Collider2D defaultHitBox;

    [SerializeField] private Transform bulletPoket;
    [SerializeField] private Transform cannonPoket;

    [SerializeField] private GameObject guideCannonPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float pingPongSpeed;

    private Coroutine coroutine;
    private Dictionary<BossCannon.BossState, Func<IEnumerator>> actionMap;
    private Rigidbody2D rb;
    private Vector2 targetDir;
    private Vector2 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        actionMap = new Dictionary<BossCannon.BossState, Func<IEnumerator>>()
        {
        { BossCannon.BossState.Idle, DoIdle },
        { BossCannon.BossState.Walk, DoWalk },
        { BossCannon.BossState.Jump, DoJump },
        { BossCannon.BossState.DefaultAttack, DoDefaultAttack},
        { BossCannon.BossState.GuidedAttack, DoGuidedAttack },
        { BossCannon.BossState.PingPongAttack, DoPingPongAttack}
        };
    }
    public void RequestAction(BossCannon.BossState state)
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

    private void pingPong()
    {
        float x = UnityEngine.Random.Range(-0.3f, 0.3f);

        if (collisionHandler.isGround)
        {
            targetDir = new Vector2(x, 1).normalized;
        }
        else if (collisionHandler.isCeiling)
        {
            targetDir = new Vector2(x, -1).normalized;
        }
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
    IEnumerator DoDefaultAttack()
    {
        idle();

        for (int i = 0; i < 3; i++)
        {
            var bullet = Instantiate(bulletPrefab, bulletPoket.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoGuidedAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            var cannon = Instantiate(guideCannonPrefab, cannonPoket.position, Quaternion.identity);
            GuideCannon cannonCode = cannon.GetComponent<GuideCannon>();
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoPingPongAttack()
    {
        idle();
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitUntil(() =>
    collisionHandler.isGround || collisionHandler.isCeiling);
            pingPong();
            yield return null;
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
}
