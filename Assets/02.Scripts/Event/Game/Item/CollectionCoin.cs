using UnityEngine;

public class CollectionCoin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            gameObject.SetActive(false);
            gameManager.SaveCollectionCoin();
        }
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
