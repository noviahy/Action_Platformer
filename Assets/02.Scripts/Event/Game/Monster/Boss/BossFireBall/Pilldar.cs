using System.Collections;
using UnityEngine;

public class Pilldar : MonoBehaviour
{
    [SerializeField] private float force;
    private Collider2D col;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        StartCoroutine(DoPilldarAttack());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponentInParent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
        }
    }

    IEnumerator DoPilldarAttack()
    {
        yield return new WaitForSeconds(1f);
        col.enabled = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }
}
