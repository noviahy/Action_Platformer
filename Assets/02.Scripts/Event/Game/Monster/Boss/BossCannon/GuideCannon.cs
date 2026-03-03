using UnityEngine;
using System.Collections;

public class GuideCannon : MonoBehaviour
{
    [SerializeField] private Explosion explosion;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed = 180f;
    private Player player;
    private Rigidbody2D rb;
    private Collider2D col;
    private Vector2 targetDir;
    private bool isReflected;
    private bool isFired = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    public void Init(Player playerCode)
    {
        player = playerCode;
        StartCoroutine(DoFire());
    }

    private void FixedUpdate()
    {
        if (isFired)
        {
            Vector2 desiredDir = (player.transform.position - transform.position).normalized;
          
            Vector3 newDir = Vector3.RotateTowards(
            targetDir,
            desiredDir,
            turnSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime,
            0f
        );
            targetDir = new Vector2(newDir.x, newDir.y).normalized;
        }
        rb.linearVelocity = targetDir * moveSpeed;
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
        if (collision.collider.CompareTag("Boss"))
        {
            if (!isReflected)
            {
                Physics2D.IgnoreCollision(col, collision.collider, true);
                return;
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            isFired = false;
            targetDir = new Vector2(player.Facing > 0 ? 1f : -1f, 0f);
            isReflected = true;
        }
    }
    IEnumerator DoFire()
    {
        float angle = 45f * Mathf.Deg2Rad;
        targetDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        rb.linearVelocity = targetDir * moveSpeed;

        yield return new WaitForSeconds(1f);
        isFired = true;
    }
}
