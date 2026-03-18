using UnityEngine;
using System.Collections;
public class RunMonKnockbackHandler : MonoBehaviour, IMonster
{
    [SerializeField] protected RunMonster runMonster;
    [SerializeField] protected int monsterHP;

    protected WaveManager wave;
    protected Vector2 knockbackDir;
    protected Rigidbody2D rb;

    protected float knockbackForce;
    protected float knockbackTime;
    protected Coroutine coroutine;
    public int HP { get; private set; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HP = monsterHP;
        coroutine = null;
    }
    public void Init(WaveManager waveManager)
    {
        wave = waveManager;
    }
    public void RequestWaveDead()
    {
        if (wave == null) return;
        wave.OnMonsterDead();
    }
    public void GetKnockbackInfo(Vector2 hitPoint, float knockback)
    {
        float dirX = transform.position.x - hitPoint.x > 0 ? 1f : -1f;

        Vector2 dir = new Vector2(dirX, 0).normalized;

        knockbackDir = dir;
        knockbackForce = knockback;
    }
    private void getVectorExplosion()
    {
        float angle = 30f * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(knockbackDir.x * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        knockbackDir = dir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

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
            knockbackTime = 0.4f;
            monsterHP -= 1;
            HP = monsterHP;

            doKnockback();
        }
        if (other.CompareTag("Explosion"))
        {
            knockbackTime = 1.5f;
            monsterHP -= 2;
            HP = monsterHP;
            getVectorExplosion();
            doKnockback();
        }
    }
    private void doKnockback()
    {
        if (coroutine != null) return;

        runMonster.ChangeMonState(RunMonster.MonsterState.Knockback);
        coroutine = StartCoroutine(Knockback());
    }
    IEnumerator Knockback()
    {
        rb.linearVelocity = Vector2.zero;

        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);

        runMonster.ChangeMonState(RunMonster.MonsterState.Idle);
        coroutine = null;
    }
}
