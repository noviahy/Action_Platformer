using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] CoinHPManager coinHPManager;
    [SerializeField] Player player;

    private void Awake()
    {
        coinHPManager = CoinHPManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            coinHPManager.Damage();
        }

        if (other.CompareTag("Explosion"))
        {
            coinHPManager.Damage();
        }

        if (other.CompareTag("BombItem"))
        {
            player.GetBoom();
        }
    }
}
