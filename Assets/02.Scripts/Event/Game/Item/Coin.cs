using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinHPManager coinHPManager;

    private void Start()
    {
       coinHPManager = CoinHPManager.Instance;
    }
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
