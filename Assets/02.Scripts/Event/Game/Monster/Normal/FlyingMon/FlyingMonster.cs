using System.Collections;
using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] FlyingMonKnockbackHandler knockbackHandler;

    [SerializeField] private float activeDis;
    [SerializeField] private float flyingSpeed;
    [SerializeField] private float jumpingForce;
    [SerializeField] float flapSpeed = 6f;   // ÆÛ´ö ¼Óµµ
    [SerializeField] float flapPower = 2f;   // ÆÛ´ö ¼¼±â
    [SerializeField] private float flapTimer;

    private GameManager gameManager;
    private Rigidbody2D rb;

    private Vector2 spawnPoint;
    private Vector2 targetPoint;
    private Vector2 diff;

    private bool isActive = false;

    public FlyingMonState CurrentState { get; private set; }
    public enum FlyingMonState
    {
        Idle,
        Flying,
        Stop,
        Knockback
    };
    public void ChangeState(FlyingMonState state)
    {
        CurrentState = state;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        spawnPoint = transform.position;
        CurrentState = FlyingMonState.Idle;
        rb.gravityScale = 0;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        diff = player.transform.position - transform.position;

        float distSqr = diff.sqrMagnitude;
        if (CurrentState == FlyingMonState.Stop || CurrentState == FlyingMonState.Knockback)
            return;

        if (distSqr < activeDis * activeDis)
        {
            isActive = true;
            ChangeState(FlyingMonState.Flying);
            targetPoint = player.transform.position + Vector3.up * 1.5f;
        }
        else
        {
            isActive = false;
            targetPoint = spawnPoint;
        }

        if (!isActive && Vector2.Distance(transform.position, spawnPoint) < 0.05f)
        {
            ChangeState(FlyingMonState.Idle);
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

        switch (CurrentState)
        {
            case FlyingMonState.Idle:
                rb.linearVelocity = Vector2.zero;
                break;
            case FlyingMonState.Flying:
                HandleMovement();
                break;
            case FlyingMonState.Stop:
                rb.linearVelocity = Vector2.zero;
                targetPoint = new Vector2(transform.position.x, rb.linearVelocity.y);
                break;
            case FlyingMonState.Knockback:
                break;
            default:
                Debug.LogError($"Unhandled MonsterState: {CurrentState}");
                break;
        }
    }
    private void HandleMovement()
    {
        Vector2 dir = (targetPoint - (Vector2)transform.position).normalized;

        float raw = Mathf.Sin(flapTimer);

        if (raw >= 0)
            flapTimer += Time.deltaTime * flapSpeed * 2f;   // ºü¸¥ »ó½Â
        else
            flapTimer += Time.deltaTime * flapSpeed * 1f; // ´À¸° ÇÏ°­

        float flap = raw * flapPower;

        rb.linearVelocity = new Vector2(
            dir.x * flyingSpeed,
            dir.y * flyingSpeed + flap
        );
    }
}
