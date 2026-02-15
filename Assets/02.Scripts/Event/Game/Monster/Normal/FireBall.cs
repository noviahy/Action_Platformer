using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float force;
    private Player player;
    private Rigidbody rb;
    private Vector2 direction;
    public void Init(Vector2 diff, Player playerCode)
    {
        rb = GetComponent<Rigidbody>();
        direction = diff;
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
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            rb.linearVelocity = new Vector2(player.Facing * moveSpeed * 2, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
