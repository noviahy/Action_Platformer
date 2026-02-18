using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private SpriteRenderer boxRenderer;
    [SerializeField] private GameObject itemBox;
    private SpriteRenderer boxColor;
    private bool isActive = true;
    private void Start()
    {
        boxColor = boxRenderer;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (!isActive) return;
            isActive = false;

            var item = Instantiate(itemPrefab, itemBox.transform.position, Quaternion.identity);
            var itemCode = item.GetComponent<BombItem>();

            itemCode.RequestSpawnItem();
            boxColor.color = Color.gray;

        }
    }
}
