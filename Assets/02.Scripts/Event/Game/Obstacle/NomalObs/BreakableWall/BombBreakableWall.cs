using UnityEngine;

public class BombBreakableWall : MonoBehaviour
{
    [SerializeField] GameObject wreckPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Explosion"))
        {
            GameObject wreck = Instantiate(wreckPrefab, transform.position, Quaternion.identity);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
