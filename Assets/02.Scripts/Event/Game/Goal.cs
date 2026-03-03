using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float heigth;
    private GameManager gameManager;
    private Vector2 startPos;
    private void Start()
    {
        gameManager = GameManager.Instance;
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        float yOffset = Mathf.Sin(Time.deltaTime * speed) * heigth;
        transform.position = startPos + new Vector2 (0, yOffset);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.ChangeState(GameManager.GameState.Clear);
            this.enabled = false;
        }
    }
}
