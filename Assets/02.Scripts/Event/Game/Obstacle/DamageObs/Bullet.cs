using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float force;
    private Collider2D col;
    private Rigidbody2D rb;
    private Vector2 dir;
    private bool isReflected = false;
    private Player player;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    public void Init(Player playerCode, int X)
    {
        dir = Vector2.right * X;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dir * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (isReflected)
            {
                Physics2D.IgnoreCollision(col, collision.collider, true);
                return;
            }
            else
            {
                var player = collision.collider.GetComponentInParent<PlayerKnockbackHandler>();
                player.GetKnockbackInfo(transform.position, force);
            }
        }
        if (collision.collider.CompareTag("Monster"))
        {
            if (!isReflected)
            {
                Physics2D.IgnoreCollision(col, collision.collider, true);
                return;
            }
            else
            {
                var boss = collision.collider.GetComponentInParent<BossCannon>();
                if (boss != null)
                {
                    boss.RequestDamage(1);
                }
            }
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            dir = new Vector2(player.Facing > 0 ? 1f : -1f, 0f);
            isReflected = true;
        }
    }
}
