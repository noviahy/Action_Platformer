using System.Collections;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private float fallGravity;
    [SerializeField] private float activeDisX = 0.3f;
    [SerializeField] private float activeDisY = 10f;
    [SerializeField] private float upSpead;

    private Rigidbody2D rb;
    private Vector2 defaultPoint;

    private bool isFalling = false;
    private bool isReturning = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultPoint = transform.position;
    }
    private void Update()
    {
        float diffX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float diffY = player.transform.position.y - transform.position.y;
        float diffYAbs = Mathf.Abs(diffY);

        if (diffX < activeDisX && diffY < 0 && diffYAbs < activeDisY && !isReturning)
            isFalling = true;
    }

    private void FixedUpdate()
    {
        if (isFalling || isReturning)
            return;

        rb.gravityScale = fallGravity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isFalling = false;
            StartCoroutine(ReturnToPoint());
        }
    }
    IEnumerator ReturnToPoint()
    {
        isReturning = true;

        float time = 0f;

        Vector2 ground = transform.position;
        rb.gravityScale = 0f;

        while (time <= 1f)
        {
            float y = Mathf.Lerp(ground.y, defaultPoint.y, time);

            transform.position = new Vector2(transform.position.x, y);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = defaultPoint;

        isFalling = false;
        isReturning = false;
    }
}
