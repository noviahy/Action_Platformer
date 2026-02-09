using UnityEngine;

public class BombItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {
            gameObject.SetActive(false);
        }
    }
}
