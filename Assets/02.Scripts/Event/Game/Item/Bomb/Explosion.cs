using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] float force = 5f;
    [SerializeField] LayerMask affectLayer;

    [SerializeField] ParticleSystem explosionFX;

    public void Explode()
    {
        showEffect();
        doEsplosion();

        Destroy(gameObject);
    }

    private void showEffect()
    {
        if (explosionFX == null) return;

        ParticleSystem fx =
    Instantiate(explosionFX, transform.position, Quaternion.identity);

        fx.Play();
        Destroy(fx.gameObject, fx.main.duration);
    }

    private void doEsplosion()
    {
        Collider2D[] hits =
    Physics2D.OverlapCircleAll(transform.position, radius, affectLayer);

        foreach (var hit in hits)
        {
            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb == null) continue;

            Vector2 dir = (rb.position - (Vector2)transform.position).normalized;
            rb.AddForce(dir * force, ForceMode2D.Impulse);
        }
    }
}
