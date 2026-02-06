using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform BombSoket;
    public Transform PutBombSoket;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private BombAttack bombAttack;
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private float throwForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float deshSpeed;

    private IAttackStratgy currentAttack;
    private IAttackStratgy defaultAttack;

    private Collider2D col;
    private Rigidbody2D rb;

    public int Facing { get; private set; } = 1;
    public Vector3 PlayerLocation { get; private set; }
    public EAttackType AttackType { get; private set; }
    public enum EAttackType
    {
        Default,
        PutBomb
    }
    public PlayerStateFlags Flags { get; private set; }

    [System.Flags]
    public enum PlayerStateFlags
    {
        None = 0,
        Idle = 1 << 0,
        Walk = 1 << 1,
        Jump = 1 << 2,
        Attack = 1 << 3,
    }

    public void AddFlags(PlayerStateFlags Flag)
    {
        Flags |= Flag;
    }
    public void RemoveFlags(PlayerStateFlags Flag)
    {
        Flags &= ~Flag;
    }

    private void Start()
    {
        PlayerLocation = transform.position;
        col = gameObject.GetComponent<Collider2D>();
        rb = col.GetComponent<Rigidbody2D>();
        AddFlags(PlayerStateFlags.Idle);
    }
    private void FixedUpdate()
    {
        if (inputManager.isDash)
        {
            Dash();
        }
        if (!inputManager.isDash)
        {
            rb.gravityScale = 1.0f;
            if ((Flags & PlayerStateFlags.Walk) != 0)
            {
                Walk(Mathf.Sign(inputManager.moveX));
            }
            if ((Flags & PlayerStateFlags.Jump) != 0)
            {
                Jump();
            }
            if ((Flags & PlayerStateFlags.Attack) != 0)
            {
                Attack();
            }
        }
        PlayerLocation = transform.position;
    }
    public void Walk(float moveX)
    {
        Facing = (int)moveX;
        rb.linearVelocity = new Vector2(
    Facing * walkSpeed,
    rb.linearVelocity.y
);
        PlayerLocation = transform.position;
    }
    public void Jump()
    {

        rb.linearVelocity = Vector2.up * jumpForce;
        PlayerLocation = transform.position;
    }

    public void Dash()
    {
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(Facing * deshSpeed, rb.linearVelocity.y);
    }
    public void Attack()
    {
        currentAttack.Attack(AttackType);
        if (currentAttack != defaultAttack)
        {
            currentAttack = defaultAttack;
            AttackType = EAttackType.Default;
        }
    }
    public void GetBoom()
    {
        GameObject obj = Instantiate(
        bombPrefab,
        BombSoket.position,
        Quaternion.identity,
        BombSoket);

        bombAttack.Init(this, obj, throwForce);

        currentAttack = bombAttack;
        AttackType = EAttackType.PutBomb;
    }
    private void OnTriggerEnter(Collider other)
    {
        playerTrigger.CollisionPlayer(other);
    }
}
