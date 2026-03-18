using UnityEngine;

public class ForPlayerTirgger_Explode : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] RunMonKnockbackHandler runMonster;
    private bool isDead = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isDead) return;

            isDead = true;
            Instantiate(explosion, transform.position, Quaternion.identity);
            runMonster.RequestWaveDead();
            gameObject.SetActive(false);
        }

    }
}
