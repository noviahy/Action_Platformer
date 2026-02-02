using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerTrigger playerTrigger;
    public Vector3 playerLocation { get; private set; }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
            playerTrigger.CollisionPlayer(other);
    }
}
