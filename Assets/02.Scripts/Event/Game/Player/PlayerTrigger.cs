using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] CoinHPManager coinHPManager;
    [SerializeField] Player player;
    [SerializeField] PlayerKnockbackHandler knockbackHandler;

    private void Awake()
    {
        coinHPManager = CoinHPManager.Instance;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Monster"))
        {
            if (!knockbackHandler.isEnable) return;
            coinHPManager.Damage();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombItem"))
        {
            player.GetBoom();
        }

        if (other.CompareTag("Explosion"))
        {
            if (!knockbackHandler.isEnable) return;
            coinHPManager.Damage();
        }
    }
}
