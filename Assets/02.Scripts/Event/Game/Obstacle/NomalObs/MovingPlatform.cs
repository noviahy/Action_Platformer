using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float acceleration;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.RequestSetAcceleration(acceleration);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.RequestNoAcceleration();
        }
    }
}
