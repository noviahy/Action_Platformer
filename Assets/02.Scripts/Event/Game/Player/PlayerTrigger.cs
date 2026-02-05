using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CoinHPManager coinHPManager;
    [SerializeField] Player player;

    public void CollisionPlayer(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            coinHPManager.Damage();
        }

        if (other.CompareTag("explosion"))
        {
            coinHPManager.Damage();
        }

        if (other.CompareTag("bomb"))
        {
            player.GetBoom();
        }
    }
}
