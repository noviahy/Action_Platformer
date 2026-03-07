using UnityEngine;

public class ForPlayerTirgger_Explode : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] RunMonKnockbackHandler runMonster;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            runMonster.RequestWaveDead();
            gameObject.SetActive(false);
        }

    }
}
