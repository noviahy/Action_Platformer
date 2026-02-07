using System.Collections;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private Explosion explosion;
    private Coroutine coroutine;
    private void Start()
    {
        coroutine = StartCoroutine(CountDown());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Monster"))
        {
            StopCoroutine(coroutine);
            explosion.Explode();
            Destroy(gameObject);
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(4);
        explosion.Explode();
        Destroy(gameObject);
        yield return null;
    }
}
