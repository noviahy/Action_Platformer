using System.Collections;
using UnityEngine;

public class NormalMonster : MonoBehaviour, IMonster
{
    [SerializeField] Player player;
    [SerializeField] int monsterHP;
    [SerializeField] int moveX;
    [SerializeField] float walkSpeed;
    [SerializeField] float force;
    [SerializeField] float activeDis;

    private GameManager gameManager;
    private Rigidbody2D rb;

    private Vector2 knockbackDir;

    private Vector2 diff;
    private bool isActive;

    private float knockbackForce;
    private float knockbackTime;

    private bool isAttacked = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        diff = player.transform.position - transform.position;

        if (diff.sqrMagnitude < activeDis * activeDis)
            isActive = true;
        else if (diff.sqrMagnitude > activeDis * activeDis)
            isActive = false;
    }

    private void FixedUpdate()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (!isActive) return;

        if (monsterHP <= 0 && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        if (isAttacked) return;
        Walk();
    }

    private void Walk()
    {
        rb.linearVelocity = new Vector2(moveX * walkSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
        }

        if (collision.collider.CompareTag("Monster"))
        {
            moveX *= -1;
        }

        if (collision.collider.CompareTag("FireBall"))
        {
            knockbackTime = 0.3f;
            monsterHP -= 1;

            doKnockback();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            isAttacked = true;
            knockbackTime = 0.2f;
            monsterHP -= 1;
            doKnockback();
        }
        if (other.CompareTag("Explosion"))
        {
            knockbackTime = 1.5f;
            isAttacked = true;
            monsterHP -= 2;
            getVectorExplosion();
            doKnockback();
        }
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
    private void doKnockback()
    {
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);
        isAttacked = false;
    }
}
