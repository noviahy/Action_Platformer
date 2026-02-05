using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            gameManager.ChangeState(GameManager.GameState.Clear);
        }
    }
}
