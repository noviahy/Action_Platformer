using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private Explosion explosion;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveX = 1f;
    private Player player;
    private Rigidbody2D rb;
    private Collider2D col;
    private Vector2 dir;
    private bool isReflected;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = Vector2.right * moveX;
        col = GetComponent<Collider2D>();
    }
    public void Init(Player playerCode)
    {
        player = playerCode;
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
        }
        if (collision.collider.CompareTag("Monster"))
        {
            if (!isReflected)
            {
                Physics2D.IgnoreCollision(col, collision.collider, true);
                return;
            }

        }
        if (collision.collider.CompareTag("Cannon"))
        {
            if (!isReflected)
            {
                Physics2D.IgnoreCollision(col, collision.collider, true);
                return;
            }
            var cannon = collision.collider.GetComponent<Cannon>();
            cannon.RequestDestroy();
        }
        Physics2D.IgnoreCollision(col, collision.collider, false);
        Instantiate(explosion, transform.position, Quaternion.identity);
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
