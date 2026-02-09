using System.Collections;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private Rigidbody2D rb;
    private SpriteRenderer bombSprite;
    private Coroutine coroutine;
    private bool isRed = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bombSprite = GetComponent<SpriteRenderer>();
        
        coroutine = StartCoroutine(CountDown());
    }
    public void GetKnockbackInfo(Vector2 hitPos, float force)
    {
        Vector2 dir = ((Vector2)transform.position - hitPos).normalized;
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Monster"))
        {
            StopCoroutine(coroutine);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator CountDown()
    {
        float totalTime = 4f;
        float elapsed = 0f;

        float startInterval = 0.7f;
        float endInterval = 0.2f;

        
        while (elapsed < totalTime)
        {
            // 시간 비율 (0 → 1)
            float t = elapsed / totalTime;

            // 점점 빨라지는 간격
            float interval = Mathf.Lerp(startInterval, endInterval, t);

            isRed = !isRed;
            bombSprite.color = isRed ? Color.red : Color.white;

            yield return new WaitForSeconds(interval);
            elapsed += interval;

        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
