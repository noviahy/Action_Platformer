using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour
{
    [SerializeField] GameObject lavaPoolPrefab;
    private Rigidbody2D rb;
    private Vector2 dir;
    private float force;
    private float height;

    private void Start()
    {
        Collider2D col = GetComponent<Collider2D>();

        height = col.bounds.size.y / 2;
    }
    public void Init(Vector2 direction, float throwForce)
    {
        rb = GetComponent<Rigidbody2D>();
        dir = direction;
        force = throwForce;
        throwLava();
    }
    private void throwLava()
    {
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Vector2 point = collision.contacts[0].point;
            Vector2 spawnPoint = new Vector2(point.x, point.y + height);

            Instantiate(lavaPoolPrefab, point, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
            Destroy(gameObject);
        }
    }
}
