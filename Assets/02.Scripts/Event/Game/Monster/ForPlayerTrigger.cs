using UnityEngine;

public class ForPlayerTrigger : MonoBehaviour
{
    [SerializeField] private float force;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
        }
    }
}
