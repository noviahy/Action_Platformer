using UnityEngine;

public class Thorn : MonoBehaviour
{
    [SerializeField] private float force;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerKnockbackHandler>();

            player.GetKnockbackInfo(transform.position, force);
        }
    }

}
