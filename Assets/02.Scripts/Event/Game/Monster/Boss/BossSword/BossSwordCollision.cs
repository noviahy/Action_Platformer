using UnityEngine;

public class BossSwordCollision : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private int BossHP;
    public bool isGround { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
        if (collision.collider.CompareTag("FireBall"))
        {
            BossHP -= 1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
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
