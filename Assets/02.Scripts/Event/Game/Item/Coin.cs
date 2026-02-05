using UnityEngine;

public class Coin : MonoBehaviour
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
}
