using UnityEngine;

public class BossCollision : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private BossSword boss;
    public bool isGround { get; private set; }
    public bool isCeiling { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
        if (collision.collider.CompareTag("Ceiling"))
        {
            isCeiling = true;
        }
        if (collision.collider.CompareTag("FireBall"))
        {
            boss.BossHP -= 1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }
        if (collision.collider.CompareTag("Ceiling"))
        {
            isCeiling = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
        }
        if (collision.CompareTag("Sword"))
        {
            BossHP -= 1;
        }
        if (collision.CompareTag("Explosion"))
        {
            BossHP -= 2;
        }
    }
}
