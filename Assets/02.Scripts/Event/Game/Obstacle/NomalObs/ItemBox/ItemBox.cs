using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private SpriteRenderer boxRenderer;
    [SerializeField] private GameObject itemBox;
    [SerializeField] private int bombNum =1;
    private SpriteRenderer boxColor;
    private bool isActive = true;
    private int num = 0;
    private void Start()
    {
        boxColor = boxRenderer;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            num++;
            if (!isActive) return;
            if (num == bombNum)
                isActive = false;
            
            var item = Instantiate(itemPrefab, itemBox.transform.position, Quaternion.identity);
            var itemCode = item.GetComponent<BombItem>();

            itemCode.RequestSpawnItem();
            boxColor.color = Color.gray;

        }
    }
}
