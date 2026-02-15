using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] PlayerAttack player;
    [SerializeField] PlayerKnockbackHandler knockbackHandler;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombItem"))
        {
            player.GetBoom();
        }
    }
}
