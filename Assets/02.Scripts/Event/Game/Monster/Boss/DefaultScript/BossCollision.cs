using UnityEngine;

public class BossCollision : MonoBehaviour
{
    [SerializeField] private float force;
    private IBoss boss;
    public bool justHit { get; private set; }
    public bool isGround { get; private set; }
    public bool isCeiling { get; private set; }
    private void Start()
    {
        boss = GetComponent<IBoss>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
            justHit = true;
        }
        if (collision.collider.CompareTag("Ceiling"))
        {
            isCeiling = true;
            justHit = true;
        }
        if (collision.collider.CompareTag("FireBall"))
        {
            boss.RequestDamage(1);
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
        if (collision.CompareTag("Sword"))
        {
            boss.RequestDamage(1);
        }
        if (collision.CompareTag("Explosion"))
        {
            boss.RequestDamage(2);
        }
    }

    public void ResetHit()
    {
        justHit = false;
    }
}
