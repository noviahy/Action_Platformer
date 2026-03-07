using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinHPManager coinHPManager;
    private bool isCollected = false;

    private void Start()
    {
       coinHPManager = CoinHPManager.Instance;
    }
    // ¸ÔÈ÷¸é »ç¶óÁü
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected)
            return;

        if (other.CompareTag("Player"))
        {
            isCollected = true;
            coinHPManager.AddCoin();
            gameObject.SetActive(false);
        }
    }
}
