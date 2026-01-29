using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CoinHPManager coinHPManager;

    private void OnTriggerEnter(Collider other)
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

        }
    }
}
