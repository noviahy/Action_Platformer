using UnityEngine;

public class BombBreakableWall : MonoBehaviour
{
    [SerializeField] GameObject wreckPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            GameObject wreck = Instantiate(wreckPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
