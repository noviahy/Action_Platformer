using UnityEngine;

public class BombItem : MonoBehaviour, IItem
{
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
