using UnityEngine;
using System.Collections;
public class BombItem : MonoBehaviour
{
    [SerializeField] private float upLength;
    [SerializeField] private float moveSpeed;
    private bool isActive = true;
    public void RequestSpawnItem()
    {
        isActive = false;

        StartCoroutine(SpawnItem());
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator SpawnItem()
    {
        Vector2 start = transform.localPosition;
        Vector2 end = start + Vector2.up * upLength;

        float time = 0f;

        while (time < 1f)
        {
            transform.localPosition = Vector2.Lerp(start, end, time);
            time += Time.deltaTime * 2f;
            yield return null;
        }
        transform.localPosition = end;
        isActive = true;
    }
}
