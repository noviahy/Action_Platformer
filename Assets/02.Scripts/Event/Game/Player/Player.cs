using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    public Transform BombSoket;
    public Transform PutBombSoket;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private BombAttack bombAttack;
    [SerializeField] private SwordAttack swordAttack;
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private float throwForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float deshSpeed;

    private IAttackStratgy currentAttack;
    private IAttackStratgy defaultAttack;

    private Rigidbody2D rb;

    private bool requestDash = false;
    private bool requestAttack = false;
    private bool requestJump = false;

    private bool lockDash = false;
    private bool lockAttack = false;
    private bool getBomb = false;
    private bool jumpDash = false;
    private bool lockWalk = false;

    public int Facing { get; private set; } = 1;
    public Vector3 PlayerLocation { get; private set; }
    public EAttackType AttackType { get; private set; }
    public enum EAttackType
    {
        Default,
        PutBomb
    }
    private void Start()
    {
        PlayerLocation = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        AttackType = EAttackType.Default;
        currentAttack = swordAttack;
    }
    private void OnEnable()
    {
        inputManager = InputManager.Instance;

        if (InputManager.Instance != null)
            InputManager.Instance.SetPlayer(this);
    }

    private void FixedUpdate()
    {
        if (InputManager.Instance == null) return;

        PlayerLocation = transform.position;

        if (groundCheck.IsGrounded)
        {
            jumpDash = false;
        }

        if (!lockWalk)
            walk(inputManager.moveX);

        if (requestDash && !getBomb && !jumpDash)
        {
            requestDash = false;
            if (lockDash || jumpDash) return;

            lockDash = true;
            if (!groundCheck.IsGrounded)
                jumpDash = true;
            lockWalk = true;
            dash();
            StartCoroutine(WaitForNextDesh());
        }
        if (requestAttack && !lockDash)
        {
            requestAttack = false;
            if (lockAttack) return;

            lockAttack = true;
            attack();
            StartCoroutine(WaitForNextAttack());
        }
        if (requestJump)
        {
            requestJump = false;
            if (!groundCheck.IsGrounded) return;
            jump();
        }
    }
    public void ChangeAttackType()
    {
        AttackType = EAttackType.PutBomb;
    }
    public void GetBoom()
    {
        getBomb = true;
        GameObject obj = Instantiate(
        bombPrefab,
        BombSoket.position,
        Quaternion.identity,
        BombSoket);

        bombAttack.Init(this, obj, throwForce);

        currentAttack = bombAttack;
    }
    public void RequestJump()
    {
        requestJump = true;
    }
    public void RequestDash()
    {
        requestDash = true;
    }
    public void RequestAttack()
    {
        requestAttack = true;
    }
    private void walk(float moveX)
    {
        if (moveX != 0)
        {
            Facing = (int)moveX;
        }

        rb.linearVelocity = new Vector2(
    moveX * walkSpeed,
    rb.linearVelocity.y
);
        PlayerLocation = transform.position;
    }
    private void jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
        PlayerLocation = transform.position;
    }

    private void dash()
    {
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(Facing * deshSpeed, 0);
    }
    private void attack()
    {
        currentAttack.Attack(AttackType);
        getBomb = false;

        if (currentAttack != defaultAttack)
        {
            currentAttack = defaultAttack;
            AttackType = EAttackType.Default;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerTrigger.CollisionPlayer(other);
    }
    IEnumerator WaitForNextDesh()
    {
        yield return new WaitForSeconds(0.3f);

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        rb.gravityScale = 1f;
        lockWalk = false;
        yield return new WaitForSeconds(0.5f);
        
        lockDash = false;
    }
    IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(0.5f);
        lockAttack = false;
    }
}
