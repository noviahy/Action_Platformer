using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BossFireBallPattern : MonoBehaviour
{

    [SerializeField] BossFireBall boss;
    [SerializeField] BossFireBallTimer bossTimer;
    [SerializeField] BossCollision collisionHandler;

    [SerializeField] Transform[] pillarPoints;
    [SerializeField] Transform[] fireBallSokets;
    [SerializeField] Transform randomSokets;

    [SerializeField] GameObject pillarPrefab;
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] GameObject lavaPrefab;

    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float lavaMaxForce;
    [SerializeField] private float lavaMinForce;

    private Coroutine coroutine;
    private Dictionary<BossFireBall.BossState, Func<IEnumerator>> actionMap;
    private Rigidbody2D rb;

    private int moveX;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        actionMap = new Dictionary<BossFireBall.BossState, Func<IEnumerator>>()
        {
        { BossFireBall.BossState.Idle, DoIdle },
        { BossFireBall.BossState.Walk, DoWalk },
        { BossFireBall.BossState.Jump, DoJump },
        { BossFireBall.BossState.DefaultAttack, DoDefaultAttack},
        { BossFireBall.BossState.PillarAttack, DoPillarAttack },
        { BossFireBall.BossState.LandomAttack, DoLandomAttack}
        };
    }
    public void RequestAction(BossFireBall.BossState state)
    {
        if (coroutine != null)
            return;
        coroutine = StartCoroutine(actionMap[state]());
    }
    private void getRandomMoveX()
    {
        moveX = UnityEngine.Random.Range(0, 2) == 0? -1 : 1;
    }
    private void idle()
    {
        rb.linearVelocity = Vector2.zero;
    }
    private void jump()
    {
        float angle = 30f * Mathf.Deg2Rad;
        getRandomMoveX();
        Vector2 dir = new Vector2(moveX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);
    }
    private void walk()
    {
        rb.linearVelocity = new Vector2(moveX * walkSpeed, rb.linearVelocity.y);
    }
    private void spawnFireBall(int i)
    {
        Instantiate(fireBallPrefab, fireBallSokets[i].position, Quaternion.identity);
    }
    private void spawnPilldar()
    {
        int rand = UnityEngine.Random.Range(0, pillarPoints.Length);
        Instantiate(pillarPrefab, pillarPoints[rand].position, Quaternion.identity);
    }
    private void spawnLava()
    {
        float angle = UnityEngine.Random.Range(45f, 135f);
        float rad = Mathf.Rad2Deg * angle;

        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        float force = UnityEngine.Random.Range(lavaMinForce, lavaMaxForce);
        var lava = Instantiate(lavaPrefab, randomSokets.position, Quaternion.identity);
        var lavaCode = lava.GetComponent<Lava>();
        lavaCode.Init(dir, force);
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
        int jumpTime = (int)bossTimer.GetJumpTime();
        for (int i = 0; i < jumpTime; i++)
        {
            jump();
            // 땅에서 떨어질 때까지 기다림
            while (collisionHandler.isGround)
                yield return null;

            // 다시 착지할 때까지 기다림
            while (!collisionHandler.isGround)
                yield return null;
        }

        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoDefaultAttack()
    {
        idle();

        for (int i = 0; i < fireBallSokets.Length; i++)
        {
            spawnFireBall(i);
            yield return new WaitForSeconds(0.7f);
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoPillarAttack()
    {
        idle();
        for (int i = 0; i <=6; i++)
        {
            spawnPilldar();
            yield return new WaitForSeconds(1f);
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
    IEnumerator DoLandomAttack() 
    {
        idle();

        for (int i = 0; i < 10; i++)
        {
            spawnLava();
            yield return new WaitForSeconds(0.5f);
        }

        idle();
        yield return new WaitForSeconds(1f);
        coroutine = null;
        boss.GetNextPattern();
    }
}
