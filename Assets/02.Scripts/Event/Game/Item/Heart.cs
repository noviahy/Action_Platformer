using UnityEngine;

public class Heart : MonoBehaviour
{
    private CoinHPManager coinHPManager;

    private void Start()
    {
        coinHPManager = CoinHPManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinHPManager.GetHP();
            gameObject.SetActive(false);
        }
    }
}
