using UnityEngine;

public class Goal : MonoBehaviour, IItem
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            gameManager.ChangeState(GameManager.GameState.Clear);
        }
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
