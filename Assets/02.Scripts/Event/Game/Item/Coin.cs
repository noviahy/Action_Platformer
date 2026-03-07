using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinHPManager coinHPManager;

    private void Start()
    {
       coinHPManager = CoinHPManager.Instance;
    }
    // ¸ÔÈ÷¸é »ç¶óÁü
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            coinHPManager.AddCoin();
            gameObject.SetActive(false);
        }
    }
}
