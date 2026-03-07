using UnityEngine;

public class ForPlayerTrigger_Run : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float runForce;
    [SerializeField] private RunMonster runMonster;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<PlayerKnockbackHandler>();
            if (runMonster.CurrentState == RunMonster.MonsterState.Run)
            {
                player.GetKnockbackInfo(transform.position, runForce);
                return;
            }
            player.GetKnockbackInfo(transform.position, force);
        }
    }
}
