using UnityEngine;
using System.Collections;
public class FlyingMonKnockbackHandler : MonoBehaviour, IMonster
{
    [SerializeField] FlyingMonster flyingMonster;
    [SerializeField] int monsterHP;
    [SerializeField] float force;

    private Vector2 knockbackDir;

    private Rigidbody2D rb;

    private float knockbackForce;
    private float knockbackTime;
    public Coroutine knockbackCoroutine { get; private set; }
    public Coroutine crashCorountine { get; private set; }
    public int HP { get; private set; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HP = monsterHP;
    }
    public void GetKnockbackInfo(Vector2 hitPoint, float knockback)
    {
        Vector2 dir = ((Vector2)transform.position - hitPoint).normalized;

        knockbackDir = dir;
        knockbackForce = knockback;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
            DoCrash();
        }
        if (collision.collider.CompareTag("FireBall"))
        {
            knockbackTime = 0.3f;
            monsterHP -= 1;
            HP = monsterHP;

            doKnockback();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            knockbackTime = 0.1f;
            monsterHP -= 1;
            HP = monsterHP;

            doKnockback();
        }
        if (other.CompareTag("Explosion"))
        {
            knockbackTime = 1.5f;
            monsterHP -= 2;
            HP = monsterHP;
            doKnockback();
        }
    }
    private void doKnockback()
    {
        if (knockbackCoroutine != null) return;

        flyingMonster.ChangeState(FlyingMonster.FlyingMonState.Knockback);
        knockbackCoroutine = StartCoroutine(Knockback());
    }
    public void DoCrash()
    {
        if(crashCorountine != null) 
            return;
        flyingMonster.ChangeState(FlyingMonster.FlyingMonState.Stop);
        crashCorountine = StartCoroutine(WaitForMove());
    }
    IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(2f);
        crashCorountine = null;
        flyingMonster.ChangeState(FlyingMonster.FlyingMonState.Flying);
    }
    IEnumerator Knockback()
    {
        rb.linearVelocity = Vector2.zero;

        rb.gravityScale = 1f;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);
        rb.gravityScale = 0f;

        knockbackCoroutine = null;
        flyingMonster.ChangeState(FlyingMonster.FlyingMonState.Flying);
    }
}
