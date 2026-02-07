using System.Collections;
using UnityEngine;
/*
NOTE:
This Player logic is intentionally implemented with bool-based control.
Refactoring to State Machine is planned for next project.
*/
public class Player : MonoBehaviour
{
    public Transform PutBombSoket;
    [SerializeField] private Transform BombSoket;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private BombAttack bombAttack;
    [SerializeField] private SwordAttack swordAttack;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject swordObject;

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
    private bool lockWalkJump = false;

    private BoxCollider2D box;

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
        bombAttack = new BombAttack();
    }
    private void OnEnable()
    {
        inputManager = InputManager.Instance;
        box = swordObject.GetComponent<BoxCollider2D>();

        if (InputManager.Instance != null)
            InputManager.Instance.SetPlayer(this);

        swordAttack.Init(swordObject, this);
        defaultAttack = swordAttack;
        currentAttack = defaultAttack;
    }

    private void FixedUpdate()
    {
        if (InputManager.Instance == null) return;

        PlayerLocation = transform.position;

        if (groundCheck.IsGrounded && jumpDash)
        {
            jumpDash = false;
        }
        // Walk
        if (!lockWalkJump)
            walk(inputManager.moveX);
        // Dash
        if (requestDash)
        {
            requestDash = false;
            if (lockDash || getBomb || jumpDash) return;

            lockDash = true;
            if (!groundCheck.IsGrounded)
                jumpDash = true;
            lockWalkJump = true;
            dash();
            StartCoroutine(WaitForNextDesh());
        }
        // Attack
        if (requestAttack && !lockDash)
        {
            requestAttack = false;
            if (lockAttack) return;

            lockAttack = true;
            attack();
            StartCoroutine(WaitForNextAttack());
        }
        // Jump
        if (requestJump)
        {
            requestJump = false;
            if (!groundCheck.IsGrounded || lockWalkJump) return;
            jump();
        }
    }
    public void ChangeAttackType()
    {
        if (!getBomb) return;

        AttackType = EAttackType.PutBomb;
        RequestAttack();
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

            Vector3 local = PutBombSoket.localPosition;
            local.x = Mathf.Abs(local.x) * Facing;

            PutBombSoket.localPosition = local;
            swordObject.transform.localPosition = local;
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

        if (currentAttack != defaultAttack)
        {
            currentAttack = defaultAttack;
            AttackType = EAttackType.Default;
        }
        if (getBomb)
        {
            getBomb = false;
            currentAttack = defaultAttack;
        }
    }
    IEnumerator WaitForNextDesh()
    {
        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        rb.gravityScale = 1f;
        lockWalkJump = false;
        yield return new WaitForSeconds(0.5f);

        lockDash = false;
    }
    IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(0.3f);
        swordAttack.AttackFinish();
        lockAttack = false;
    }
}
