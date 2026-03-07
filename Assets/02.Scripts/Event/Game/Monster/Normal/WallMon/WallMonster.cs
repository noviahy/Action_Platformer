using UnityEngine;

public class WallMonster : MonoBehaviour, IMonster
{
    [SerializeField] Player player;
    [SerializeField] Rotation rotation;
    [SerializeField] int monsterHP;

    [SerializeField] float force;
    private GameManager gameManager;
    private WaveManager wave;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void FixedUpdate()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (monsterHP <= 0)
        {
            if (wave != null)
            {
                wave.OnMonsterDead();
            }
            gameObject.SetActive(false);
            return;
        }

        if (rotation.isActive)
            rotation.followPlayer();

        if (!rotation.isAttack) return;

        rotation.RequestCoroutine();
    }
    public void Init(WaveManager waveManager)
    {
        wave = waveManager;
    }
    public void GetKnockbackInfo(Vector2 hitPoint, float knockback)
    {
        /*
        float dirX = transform.position.x - hitPoint.x > 0 ? 1f : -1f;

        Vector2 dir = new Vector2(dirX, 0).normalized;

        knockbackDir = dir;
        knockbackForce = knockback;
        */
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FireBall"))
        {
            monsterHP -= 1;
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
}
