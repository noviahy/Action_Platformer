using System.Collections;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private Explosion explosion;
    public void Attack()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(4);
        explosion.Explode();
        Destroy(gameObject);
        yield return null;
    }
}
