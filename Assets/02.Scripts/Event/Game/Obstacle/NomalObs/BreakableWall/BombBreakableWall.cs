using UnityEngine;

public class BombBreakableWall : MonoBehaviour
{
    [SerializeField] GameObject wreckPrefab;
    public void BreakWall()
    {
        GameObject wreck = Instantiate(wreckPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
