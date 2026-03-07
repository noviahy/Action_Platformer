using UnityEngine;

public class CollectionCoin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private bool isCollected = false;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected) return;

        if (other.CompareTag("Player"))
        {
            isCollected = true;
            gameObject.SetActive(false);
            gameManager.SaveCollectionCoin();
        }
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
