using UnityEngine;

public class ForPlayerTrigger_Fly : MonoBehaviour
{
    [SerializeField] FlyingMonKnockbackHandler flyingMon;
    [SerializeField] float force;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<PlayerKnockbackHandler>();
            player.GetKnockbackInfo(transform.position, force);
            flyingMon.DoCrash();
        }
    }
}