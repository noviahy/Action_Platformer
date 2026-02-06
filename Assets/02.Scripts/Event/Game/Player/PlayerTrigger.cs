using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CoinHPManager coinHPManager;
    [SerializeField] Player player;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        coinHPManager = CoinHPManager.Instance;
    }

    public void CollisionPlayer(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            coinHPManager.Damage();
        }

        if (other.CompareTag("Explosion"))
        {
            coinHPManager.Damage();
        }

        if (other.CompareTag("Bomb"))
        {
            player.GetBoom();
        }
    }
}
