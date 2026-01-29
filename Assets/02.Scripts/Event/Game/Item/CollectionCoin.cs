using UnityEngine;

public class CollectionCoin : MonoBehaviour, IItem
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            gameObject.SetActive(false);
        }
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
