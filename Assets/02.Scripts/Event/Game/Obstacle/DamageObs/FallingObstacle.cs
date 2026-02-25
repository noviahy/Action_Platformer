using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private float fallGravity;
    [SerializeField] private float activeDis;
    [SerializeField] private float upSpead;
    private Rigidbody2D rb;
    private Vector2 defaultPoint;
    private Coroutine coroutine;
    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        defaultPoint = transform.position;
    }

    private void FixedUpdate()
    {
        float diffX = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (diffX < activeDis)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            rb.gravityScale = fallGravity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            coroutine = StartCoroutine(ReturnToPoint());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.RequestDead();
        }
    }
    IEnumerator ReturnToPoint()
    {
        float time = 0f;

        Vector2 ground = transform.position;

        while (time <= 1f)
        {
            float speed = Mathf.Lerp(ground.y, defaultPoint.y, time);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);

            time += Time.deltaTime;
            yield return null;
        }
        coroutine = null;
    }
}
