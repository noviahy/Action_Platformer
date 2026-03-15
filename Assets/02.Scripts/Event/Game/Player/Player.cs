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

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private PlayerKnockbackHandler knockbackHandler;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform activePoint;
    private CoinHPManager coinHPManager;

    private bool isBossActive = false;
    private bool requestDash = false;
    private bool requestAttack = false;
    private bool requestJump = false;

    private bool lockDash = false;
    private bool jumpDash = false;
    private bool lockWalkJump = false;
    public float Facing { get; private set; } = 1;
    public float acceleration {  get; private set; } = 0;

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

        if (activePoint != null && !isBossActive && Mathf.Abs(transform.position.x - activePoint.position.x) < 0.3f)
        {
            isBossActive = true;
            playerMovement.RequestStop();
        }

        if (knockbackHandler.lockInput) return;
        if (playerMovement.lockInput) return;

        if (groundCheck.IsGrounded && jumpDash)
        {
            jumpDash = false;
        } 
        // Dash
        if (requestDash)
        {
            requestDash = false;
            if (!lockDash && !jumpDash)
            {
                lockDash = true;

                if (!groundCheck.IsGrounded)
                    jumpDash = true;

                lockWalkJump = true;
                playerMovement.dash();
            }
        }  
        // Jump
        if (requestJump)
        {
            requestJump = false;
            if (!groundCheck.IsGrounded || lockWalkJump) return;
            playerMovement.jump();
        }
        // Attack
        if (requestAttack && !lockDash)
        {
            requestAttack = false;
            playerAttack.attack();
        }
        // Walk
        if (!lockWalkJump)
        {
            if (inputManager.moveX != 0)
                Facing = inputManager.moveX;

            playerMovement.walk(inputManager.moveX);
        }
    }
    public void RequestSetAcceleration(float speed)
    {
        acceleration = speed;
    }
    public void RequestNoAcceleration()
    {
        acceleration = 0;
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
