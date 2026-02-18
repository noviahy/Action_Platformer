using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float force;
    private Player player;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 direction;
    private bool isReflected = false;
    public void Init(Vector2 diff, Player playerCode)
    {
        rb = GetComponent<Rigidbody2D>();
        player = playerCode;
        direction = diff;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (direction == null) return;
        rb.linearVelocity = direction * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Monster"))
        {
            if (!isReflected)
                return;
            var monster = collision.collider.GetComponent<PlayerKnockbackHandler>();
            monster.GetKnockbackInfo(transform.position, force);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            direction = new Vector2(player.Facing > 0 ? 1f : -1f, 0f);
            isReflected = true;
            sprite.color = Color.blue;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
