using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerKnockbackHandler knockbackHandler;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombItem"))
        {
            player.GetBoom();
        }
    }
}
