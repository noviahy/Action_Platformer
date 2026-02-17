using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float force;
    private Player player;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 direction;
    private int playerLayer;
    private int monsterLayer;
    private int fireBallLayer;
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        monsterLayer = LayerMask.NameToLayer("Monster");
    }
    public void Init(Vector2 diff, Player playerCode)
    {
        rb = GetComponent<Rigidbody2D>();
        player = playerCode;
        direction = diff;
        fireBallLayer = LayerMask.NameToLayer("FireBall");
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
            Physics2D.IgnoreLayerCollision(fireBallLayer, monsterLayer, true);
            Physics2D.IgnoreLayerCollision(fireBallLayer, playerLayer, false);
            sprite.color = Color.blue;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
