using UnityEngine;

public class Wreck : MonoBehaviour
{
    [SerializeField] ParticleSystem wreckParticle;
    public void Start()
    {
        explode();
    }
    private void explode()
    {
        if (wreckParticle == null) return;

        ParticleSystem fx = Instantiate(wreckParticle, transform.position, Quaternion.identity);

        fx.Play();
        Destroy(fx.gameObject, fx.main.duration);
        Destroy(gameObject, fx.main.duration);
    }
}
