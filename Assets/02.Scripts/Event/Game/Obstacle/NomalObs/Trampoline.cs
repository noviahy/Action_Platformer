using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") && !collision.collider.CompareTag("Monster"))
            return;

        Rigidbody2D rb = collision.collider.attachedRigidbody;
        if (rb == null)
            return;

        if (rb.linearVelocity.y <= 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }
    
}
