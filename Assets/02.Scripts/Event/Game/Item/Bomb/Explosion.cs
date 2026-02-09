using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] float force = 4f;
    [SerializeField] LayerMask affectLayer;

    [SerializeField] ParticleSystem explosionFX;

    public void Start()
    {
        Explode();
    }

    public void Explode()
    {
        showEffect();
        doEsplosion();
    }

    private void showEffect()
    {
        if (explosionFX == null) return;

        ParticleSystem fx =
    Instantiate(explosionFX, transform.position, Quaternion.identity);

        fx.Play();
        Destroy(fx.gameObject, fx.main.duration);
        Destroy(gameObject, fx.main.duration);
    }

    private void doEsplosion()
    {
        Collider2D[] hits =
    Physics2D.OverlapCircleAll(transform.position, radius, affectLayer);
        
        foreach (var hit in hits)
        {
            
            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb == null) continue;

            var target = hit.GetComponent<IKnockbackHandler>();
            if (target == null) continue;
            target.GetKnockbackInfo(transform.position, force);
        }
    }
}
