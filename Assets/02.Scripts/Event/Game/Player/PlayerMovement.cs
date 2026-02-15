using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GroundCheck groundCheck;

    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float deshSpeed;

    private Coroutine coroutine;

    private float defaultGravity;
    private void Start()
    {
        defaultGravity = rb.gravityScale;

    }
    public void walk(float moveX)
    {
        if (moveX != 0)
        {
            Vector3 local = player.PutBombSoket.localPosition;
            local.x = Mathf.Abs(local.x) * player.Facing;

            player.PutBombSoket.localPosition = local;
            swordObject.transform.localPosition = local;
        }

        rb.linearVelocity = new Vector2(
    moveX * walkSpeed,
    rb.linearVelocity.y
);
    }
    public void jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }
    public void dash()
    {
        if (coroutine != null)
            return;

        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(player.Facing * deshSpeed, 0);
        coroutine = StartCoroutine(WaitForNextDesh());
    }
    IEnumerator WaitForNextDesh()
    {
        yield return new WaitForSeconds(0.2f);

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            rb.gravityScale = defaultGravity;
        }

        player.UnlockWalkJump();

        yield return new WaitForSeconds(0.4f);
        coroutine = null;
        player.UnlockDash();
    }
}
