using System.Collections;
using UnityEngine;

public class RunMonster : MonoBehaviour, IMonster
{
    [SerializeField] Player player;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] int monsterHP;
    [SerializeField] int moveX;

    [SerializeField] float force;
    [SerializeField] float runForce;
    [SerializeField] float jumpForce;

    [SerializeField] float activeDis;
    [SerializeField] float runDis;

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;


    [SerializeField] float deceleration;


    private GameManager gameManager;
    private Rigidbody2D rb;

    private Vector2 knockbackDir;
    private Vector2 diff;

    private Coroutine coroutine;

    private float decisionInterval;
    private float knockbackForce;
    private float knockbackTime;

    private float timer;

    private bool isActive = false;
    private bool isAttacked = false;
    private bool isRun = false;
    private bool doJump = false;

    private int saveMoveX;
    private int preMoveX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        decisionInterval = 3f;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (preMoveX != moveX && moveX != 0 && !isRun)
        {
            doJump = false;
        }

        diff = player.transform.position - transform.position;

        float distSqr = diff.sqrMagnitude;

        if (distSqr < runDis * runDis)
        {
            isRun = true;
            isActive = true;
        }
        else if (distSqr < activeDis * activeDis)
        {
            isRun = false;
            isActive = true;

            if (coroutine == null)
                timer += Time.deltaTime;

            if (timer >= decisionInterval)
            {
                timer = 0f;
                chooseNewDirection();
                chooseNewInterval();
            }
        }
        else
        {
            isRun = false;
            isActive = false;
        }
    }
    private void FixedUpdate()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (monsterHP <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (isAttacked) return;

        HandleMovement();
    }
    public void GetKnockbackInfo(Vector2 hitPoint, float force)
    {
        float dirX = transform.position.x - hitPoint.x > 0 ? 1f : -1f;

        Vector2 dir = new Vector2(dirX, 0).normalized;

        knockbackDir = dir;
        knockbackForce = force;
    }
    private void HandleMovement()
    {
        float targetSpeed = 0f;

        if (!isActive)
        {
            targetSpeed = 0f;
        }
        else if (isRun)
        {
            if (!doJump)
            {
                requestCoroutine();
            }

            doJump = true;
            if (groundCheck.IsGrounded)
            {
                targetSpeed = moveX * runSpeed;
            }
        }
        else
        {
            targetSpeed = moveX * walkSpeed;
        }

        float currentX = rb.linearVelocity.x;

        if (Mathf.Abs(currentX) > Mathf.Abs(targetSpeed))
        {
            float newX = Mathf.MoveTowards(
                currentX,
                targetSpeed,
                deceleration * Time.fixedDeltaTime
            );

            rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
        }
    }
    private void jump()
    {
        saveMoveX = moveX;
        moveX = 0;

        rb.linearVelocity = Vector2.up * jumpForce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerKnockbackHandler>();
            if (isRun)
            {
                player.GetKnockbackInfo(transform.position, runForce);
                return;
            }
            player.GetKnockbackInfo(transform.position, force);
        }

        if (collision.collider.CompareTag("Monster"))
        {
            moveX *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            isAttacked = true;
            knockbackTime = 0.2f;
            monsterHP -= 1;
            doknockback();
        }
        if (other.CompareTag("Explosion"))
        {
            knockbackTime = 1.5f;
            isAttacked = true;
            monsterHP -= 2;
            getVectorExplosion();
            doknockback();
        }
    }
    private void getVectorExplosion()
    {
        float angle = 30f * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(knockbackDir.x * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        knockbackDir = dir;
    }
    private void chooseNewDirection()
    {
        int random = Random.Range(0, 3);

        if (moveX != 0)
            preMoveX = moveX;

        switch (random)
        {
            case 0:
                moveX = -1;
                break;
            case 1:
                moveX = 0;
                break;
            case 2:
                moveX = 1;
                break;
        }
    }
    private void chooseNewInterval()
    {
        decisionInterval = Random.Range(1, 3);
    }
    private void requestCoroutine()
    {
        if (coroutine != null)
            return;
        moveX = diff.x > 0 ? 1 : -1;
        coroutine = StartCoroutine(Detect());
    }
    private void doknockback()
    {
        if (moveX != 0)
            preMoveX = moveX;
        StartCoroutine(Knockback());
    }
    IEnumerator Knockback()
    {
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);
        isAttacked = false;

    }
    IEnumerator Detect()
    {
        jump();

        while (!groundCheck.IsGrounded)
            yield return null;

        moveX = saveMoveX;

        yield return new WaitForSeconds(2f);
        coroutine = null;
    }
}
