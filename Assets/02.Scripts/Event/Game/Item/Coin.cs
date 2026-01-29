using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    [SerializeField] private CoinHPManager coinHPManager;
    // ¸ÔÈ÷¸é »ç¶óÁü
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            coinHPManager.AddCoin();
            gameObject.SetActive(false);
        }
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
