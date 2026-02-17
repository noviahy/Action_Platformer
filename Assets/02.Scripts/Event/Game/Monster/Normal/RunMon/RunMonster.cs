using System.Collections;
using UnityEngine;

public class RunMonster : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] RunMonKnockbackHandler knockbackHandler;
    [SerializeField] RunMonAI runMonAI;

    [SerializeField] private float jumpForce;
    [SerializeField] private float activeDis;
    [SerializeField] private float runDis;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float deceleration;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private Coroutine jumpCoroutine;

    private Vector2 diff;

    private float timer;
    public int moveX { get; private set; }
    private MonsterState previousState;
    public MonsterState CurrentState { get; private set; }
    public enum MonsterState
    {
        Idle,
        Walk,
        Run,
        Knockback,
    }
    public void ChangeMonState(MonsterState state)
    {
        if (CurrentState == state) return;

        previousState = CurrentState;
        CurrentState = state;

        if (CurrentState == MonsterState.Run && jumpCoroutine == null && previousState != MonsterState.Idle)
            requestCoroutine();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        CurrentState = MonsterState.Idle;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        diff = player.transform.position - transform.position;

        float distSqr = diff.sqrMagnitude;

        if (CurrentState == MonsterState.Knockback)
            return;

        if (distSqr < runDis * runDis)
        {
            ChangeMonState(MonsterState.Run);
        }
        else if (distSqr < activeDis * activeDis)
        {
            ChangeMonState(MonsterState.Walk);
            getRandomValue();
        }
        else
        {
            ChangeMonState(MonsterState.Idle);
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (knockbackHandler.HP <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (CurrentState == MonsterState.Knockback) return;
        if (jumpCoroutine != null) return;

        HandleMovement();
    }
    public void ChnageMoveX(int X)
    {
        moveX = X;
    }
    private void HandleMovement()
    {
        float targetSpeed = 0f;

        switch (CurrentState)
        {
            case MonsterState.Idle:
                targetSpeed = 0f;
                break;
            case MonsterState.Walk:
                targetSpeed = moveX * walkSpeed;
                break;
            case MonsterState.Run:
                targetSpeed = moveX * runSpeed;
                break;
            case MonsterState.Knockback:
                rb.linearVelocity = Vector2.zero;
                break;
            default:
                Debug.LogError($"Unhandled MonsterState: {CurrentState}");
                break;
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
    private void getRandomValue()
    {
        if (jumpCoroutine == null)
            timer += Time.deltaTime;

        if (timer >= runMonAI.decisionInterval)
        {
            timer = 0f;
            runMonAI.chooseNewDirection();
            runMonAI.chooseNewInterval();
        }
    }
    private void jump()
    {
        moveX = 0;
        rb.linearVelocity = new Vector2(0, jumpForce);
    }
    private void requestCoroutine()
    {
        if (jumpCoroutine != null)
            return;
        jumpCoroutine = StartCoroutine(Detect());
    }
    IEnumerator Detect()
    {
        jump();

        while (!groundCheck.IsGrounded)
            yield return null;
        moveX = diff.x > 0 ? 1 : -1;

        jumpCoroutine = null;
    }
}
