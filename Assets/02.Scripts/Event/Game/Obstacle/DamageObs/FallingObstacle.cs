using UnityEngine;
using static FlyingMonster;

public class FallingObstacle : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private float fallGravity;
    [SerializeField] private float activeDis;
    private LayerMask playerLayer;
    private LayerMask groundLayer;
    private Rigidbody2D rb;
    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void FixedUpdate()
    {
        float diffX = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (diffX < activeDis * activeDis)
        {
            rb.gravityScale = fallGravity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.RequestDead();
        }
        if(collision.gameObject.layer == groundLayer)
        {
            Destroy(gameObject);
        }
    }
}
