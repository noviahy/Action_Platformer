using UnityEngine;

public class CollectionCoin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
