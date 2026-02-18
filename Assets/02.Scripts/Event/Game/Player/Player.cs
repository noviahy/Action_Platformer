using UnityEngine;
/*
NOTE:
This Player logic is intentionally implemented with bool-based control.
Refactoring to State Machine is planned for next project.
*/
public class Player : MonoBehaviour
{
    public Transform PutBombSoket;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private PlayerKnockbackHandler knockbackHandler;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMovement playerMovement;
    private CoinHPManager coinHPManager;

    private bool requestDash = false;
    private bool requestAttack = false;
    private bool requestJump = false;

    private bool lockDash = false;
    private bool jumpDash = false;
    private bool lockWalkJump = false;
    public float Facing { get; private set; } = 1;
    private void OnEnable()
    {
        inputManager = InputManager.Instance;
        coinHPManager = CoinHPManager.Instance;

        if (InputManager.Instance != null)
            InputManager.Instance.SetPlayer(this);
    }
    private void FixedUpdate()
    {
        if (InputManager.Instance == null) return;

        if (knockbackHandler.lockInput) return;

        if (groundCheck.IsGrounded && jumpDash)
        {
            jumpDash = false;
        }
        // Walk
        if (!lockWalkJump)
        {
            if (inputManager.moveX != 0)
                Facing = inputManager.moveX;

            playerMovement.walk(inputManager.moveX);
        }
        // Dash
        if (requestDash)
        {
            requestDash = false;
            if (lockDash || jumpDash) return;

            lockDash = true;

            if (!groundCheck.IsGrounded)
                jumpDash = true;

            lockWalkJump = true;
            playerMovement.dash();
        }
        // Attack
        if (requestAttack && !lockDash)
        {
            requestAttack = false;
            playerAttack.attack();
        }
        // Jump
        if (requestJump)
        {
            requestJump = false;
            if (!groundCheck.IsGrounded || lockWalkJump) return;
            playerMovement.jump();
        }
    }
    public void RequestDead()
    {
        coinHPManager.Dead();
    }
    public void RequestChangeAttackType()
    {
        playerAttack.ChangeAttackType();
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
    public void UnlockWalkJump()
    {
        lockWalkJump = false;
    }
    public void UnlockDash()
    {
        lockDash = false;
    }
}
