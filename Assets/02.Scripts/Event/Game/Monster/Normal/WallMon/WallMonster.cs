using System.Collections;
using UnityEngine;

public class WallMonster : MonoBehaviour, IMonster
{
    [SerializeField] Player player;
    [SerializeField] GameObject firePrefab;
    [SerializeField] GameObject head;
    [SerializeField] int monsterHP;

    [SerializeField] float force;

    [SerializeField] float activeDis;

    private GameManager gameManager;
    private Coroutine coroutine;

    private Vector2 shootDir;

    private bool isActive = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        shootDir = player.transform.position - transform.position;

        float distSqr = shootDir.sqrMagnitude;

        isActive = distSqr < activeDis * activeDis;
    }
    private void FixedUpdate()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (monsterHP <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!isActive) return;

        followPlayer();
        if (coroutine == null)
            coroutine = StartCoroutine(StartAttack());
    }
    private void followPlayer()
    {
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        float rotationSpeed = 30f;

        head.transform.rotation = Quaternion.RotateTowards(
    transform.rotation,
    targetRotation,
    rotationSpeed * Time.deltaTime
);
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
            monsterHP -= 1;
        }
        if (other.CompareTag("Explosion"))
        {
            monsterHP -= 2;
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            GameObject fireBall = Instantiate(firePrefab, transform.position, Quaternion.identity);

            FireBall fireBallCode = fireBall.GetComponent<FireBall>();
            fireBallCode.Init(shootDir.normalized, player);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(3f);
        coroutine = null;
    }
}
