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

    [SerializeField] private Transform bulletPoket;
    [SerializeField] private Transform cannonPoket;

    [SerializeField] private GameObject guideCannonPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float pingPongSpeed;

    private Player player;
    private Coroutine coroutine;
    private Dictionary<BossCannon.BossState, Func<IEnumerator>> actionMap;
    private Rigidbody2D rb;
    private Vector2 targetDir;
    private bool firstHitIgnored = false;
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
    public void Init(Player code)
    {
        player = code;
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
            Bullet bulletCode = bullet.GetComponent<Bullet>();
            bulletCode.Init(player, boss.moveX);
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
            cannonCode.Init(player);
        }
        idle();
        yield return new WaitForSeconds(1f);

        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoPingPongAttack()
    {
        idle();
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(1f);

        targetDir = new Vector2(1, 1).normalized;

        int bounceCount = 0;

        while (bounceCount < 7)
        {
            rb.linearVelocity = targetDir * pingPongSpeed;

            if (collisionHandler.justHit)
            {
                pingPong();
                collisionHandler.ResetHit();
                if (firstHitIgnored)
                    Instantiate(explosion, transform.position, Quaternion.identity);
                firstHitIgnored = true;
                bounceCount++;
                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
        rb.gravityScale = 1f;

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
}
