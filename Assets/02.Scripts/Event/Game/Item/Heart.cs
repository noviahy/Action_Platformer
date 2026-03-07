using UnityEngine;

public class Heart : MonoBehaviour
{
    private CoinHPManager coinHPManager;
    private bool isCollected = false;
    private void Start()
    {
        coinHPManager = CoinHPManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return;

        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            coinHPManager.GetHP();
            gameObject.SetActive(false);
        }
    }
}
